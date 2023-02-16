using HandlebarsDotNet;



Handlebars.RegisterHelper("sph", (writer, context, arguments) =>
{
  writer.Write($"{arguments[0]} {arguments[1]}");
});
Handlebars.RegisterHelper("ph", (writer, context, arguments) =>
            {
                if (arguments.Length < 1)
                    throw new InvalidOperationException("Atleast one argument is mandatory while using '#ph' helper");
                var phKey = arguments[0] as string;

                var dataDict = new Dictionary<string, object>(context.Value as Dictionary<string, object>, StringComparer.OrdinalIgnoreCase);
                //OrdinalIgnoreCase is the fastest as it dont have to look for culture specific rules while comparing
                if (dataDict.ContainsKey(phKey) && !string.IsNullOrWhiteSpace(dataDict[phKey]?.ToString()))
                    writer.Write(dataDict[phKey]);
                else
                {
                    writer.WriteSafeString($"<span style='color:red'><eos-ph>{(arguments.Length > 1 ? arguments[1] : arguments[0])}</eos-ph></span>");
                }
            });

string source =
@"<div class=""entry"">
  <h1>{{Title}} {{#sph 'by' 'Rino'}}</h1>
  <div class=""body"">
    {{body}} {{ #Ph 'title' 'New Joiner
    FirstName' }}
  </div>
</div>";

var data = new Dictionary<string, object>();
data.Add("Title","Post");
data.Add("body","Sample body");

string GetRenderedText(string tmpl)
{
    string result = string.Empty;
    if (!string.IsNullOrWhiteSpace(tmpl))
    {
        var compiledTmpl = Handlebars.Compile(tmpl);
        
        result = compiledTmpl(data);
    }

    return result;
}
Console.WriteLine( string.Join(',', Handlebars.Configuration.Helpers.Select(h=>$"{h.Key} - {h.Value}")));
Console.WriteLine("--------");
Console.WriteLine(GetRenderedText(source));
