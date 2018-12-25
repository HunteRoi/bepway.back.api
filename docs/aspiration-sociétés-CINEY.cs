namespace Model
{
    public class Localisation
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}

using Model;
namespace Services
{
	public class Aspiration
	{
		var access = new OpenDataAccess();
		IEnumerable<Record> records = await access.getAllCompaniesAsync();
		List<Company> companies = new List<Company>();

		foreach (var record in records)
		{
			using (var client = new HttpClient()) 
			{
				decimal latitude;
				decimal longitude;
				TextInfo ti = Thread.CurrentThread.CurrentCulture.TextInfo;
				string normalizedCity = ti.ToTitleCase(ti.ToLower(record.Fields.AdresseVille));

				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				
				Thread.Sleep(5000);
				var response = await client.GetAsync($"https://eu1.locationiq.com/v1/search.php?key=3da3df985996f9&format=json&street={record.Fields.AdresseRueNumero}&city={normalizedCity}&postalcode={record.Fields.AdresseCodePostal}&country=Belgium");
				
				if (response.IsSuccessStatusCode) {
					var result = await response.Content.ReadAsStringAsync();
					var firstLocalisation = JValue.Parse(result).ToObject<Localisation[]>()[0];
					latitude = Decimal.Parse(firstLocalisation.Lat, CultureInfo.InvariantCulture);
					longitude = Decimal.Parse (firstLocalisation.Lon, CultureInfo.InvariantCulture);

					Model.Company new_company = new Model.Company
					{
						IdOpenData = record.RecordId,
						Name = record.Fields.NomEntreprise,
						Status = Model.Constants.Status.EXISTING,
						Address = $"{record.Fields.AdresseRueNumero}, {record.Fields.AdresseCodePostal} {normalizedCity}",
						Latitude = latitude,
						Longitude = longitude,
						CreationDate = DateTime.Parse(record.Record_timestamp, CultureInfo.InvariantCulture),
						IsPremium = false,
						ActivitySector = Context.ActivitySector.FirstOrDefault(a => a.Name.Equals(record.Fields.SecteurActivite))
					};
				
					companies.Add(new_company);
					Context.Company.Add(new_company);
				}
			}
		}
		await Context.SaveChangesAsync();
		return companies;
	}
}