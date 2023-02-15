using HandlebarsDotNet;

Handlebars.RegisterHelper("sph", (writer, context, arguments) =>
{
  writer.Write($"{arguments[0]} {arguments[1]}");
});

string source =
@"<div class=""entry"">
  <h1>{{title}} {{#sph 'by' 'Rino'}}</h1>
  <div class=""body"">
    {{body}}
  </div>
</div>";

var data = new {
    title = "Post",
    body = "A sample body"
};

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
