string csvPath = "C:/Documents/WeCloudData/Community Solar/Measure.csv";
var lines = System.IO.File.ReadAllLines(csvPath).Skip(1); 

foreach (var line in lines)
{
    var matches = System.Text.RegularExpressions.Regex.Matches(line, @"(?:^|,)(?:""([^""]*(?:""""[^""]*)*)""|([^,]*))");
    
    if (matches.Count < 4) continue;

    var tableName = matches[0].Groups[1].Value;
    if (string.IsNullOrEmpty(tableName))
    {
        tableName = matches[0].Groups[2].Value;
    }

    var measureName = matches[1].Groups[1].Value;
    if (string.IsNullOrEmpty(measureName))
    {
        measureName = matches[1].Groups[2].Value;
    }

    var formula = matches[2].Groups[1].Value;
    if (string.IsNullOrEmpty(formula))
    {
        formula = matches[2].Groups[2].Value;
    }
    
    var format = matches[3].Groups[1].Value;
    if (string.IsNullOrEmpty(format))
    {
        format = matches[3].Groups[2].Value;
    }

    formula = formula.Replace("\"\"", "\"");

    var table = Model.Tables[tableName];
    if (table == null) continue;

    try
    {
        var newMeasure = table.AddMeasure(measureName, formula);
        newMeasure.FormatString = format;
    }
    catch
    {}
}