namespace Model
{
    public class Localisation
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}

var access = new OpenDataAccess ();
IEnumerable<Record> records = await access.getAllCompaniesAsync ();
List<Company> companies = new List<Company> ();

foreach (var record in records) {
	if (!String.IsNullOrEmpty (record.Fields.AdresseVille) && !String.IsNullOrEmpty (record.Fields.AdresseRueNumero)) {
		using (var client = new HttpClient ()) {
			string latitude;
			string longitude;
			TextInfo ti = Thread.CurrentThread.CurrentCulture.TextInfo;
			string normalizedCity = ti.ToTitleCase (ti.ToLower (record.Fields.AdresseVille));

			client.DefaultRequestHeaders.Accept.Clear ();
			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			Thread.Sleep (5000);
			var response = await client.GetAsync ($"https://eu1.locationiq.com/v1/search.php?key=3da3df985996f9&format=json&street={record.Fields.AdresseRueNumero}&city={normalizedCity}&postalcode={record.Fields.AdresseCodePostal}&country=Belgium");

			if (response.IsSuccessStatusCode) {
				var result = await response.Content.ReadAsStringAsync ();
				var firstLocalisation = JValue.Parse (result).ToObject<Localisation[]> () [0];
				latitude = firstLocalisation.Lat;
				longitude = firstLocalisation.Lon;

				Model.Company new_company = new Model.Company {
					IdOpenData = record.RecordId,
					Name = record.Fields.NomEntreprise,
					Status = Model.Constants.Status.EXISTING,
					Address = $"{record.Fields.AdresseRueNumero}, {record.Fields.AdresseCodePostal} {normalizedCity}",
					CreationDate = DateTime.Parse (record.Record_timestamp, CultureInfo.InvariantCulture),
					IsPremium = false,
					ActivitySector = Context.ActivitySector.FirstOrDefault (a => a.Name.Equals (record.Fields.SecteurActivite)),
					Coordinates = new Coordinates ()
				};
				new_company.Coordinates.Latitude = latitude;
				new_company.Coordinates.Longitude = longitude;

				companies.Add (new_company);
				Context.Company.Add (new_company);
			}
		}
	}
}
await Context.SaveChangesAsync ();