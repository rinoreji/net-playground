using HandlebarsDotNet;

// Handlebars.Configuration.Compatibility.RelaxedHelperNaming=false;

Handlebars.RegisterHelper("#sph", (writer, context, arguments) =>
{
  writer.Write("'h- w'");
});

string source =
@"<div class=""entry"">
  <h1>{{#sph 'title' 'xyz'}}</h1>
  <div class=""body"">
    {{body}}
  </div>
</div>";

var data = new {
    title = "My new post",
    body = "This is my first post!"
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

Console.WriteLine(GetRenderedText(source));
