using Htmx;
var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <head>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
                <style>
                    div[hx-trigger] {
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
                <div class="container">
                    <h1>Various hx-target options</h1>
                    <p>You can find the full documentation <a href="https://v2-0v2-0.htmx.org/docs/#targets">here</a></p>
                    <div class="row">
                        <div class="col-md-3">
                            <ul>
                                <li>
                                    <strong>this keyword</strong><br/>
                                    <div hx-get="/htmx/this-keyword" hx-trigger="click" hx-target="this">Click Me</div>
                                </li>
                                <li>
                                    <strong>Tag Selector</strong><br/>
                                    <div hx-get="/htmx/tag-selector" hx-trigger="click" hx-target="article">Click Me</div>
                                </li>
                                <li>
                                    <strong>Id Selector</strong><br/>
                                    <div hx-get="/htmx/id-selector" hx-trigger="click" hx-target="#message">Click Me</div>
                                </li>
                                <li>
                                    <strong>Class Selector</strong><br/>
                                    <div hx-get="/htmx/class-selector" hx-trigger="click" hx-target=".info">Click Me</div>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-6">
                            <article class="mb-3" style="background-color:green;color:white;">article tag</article>
                            <div class="mb-3" id="message" style="background-color:magenta;color:white;">div#message</div>
                            <div class="mb-3 info" style="background-color:blue;color:white;">div.info</div>
                        </div>
                    </div>
                </div>
                <script src="https://unpkg.com/htmx.org@2.0.0/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx/{key}", (HttpRequest request, string key) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return key switch 
    {
        "this-keyword" => Results.Content($"""Hello {DateTime.UtcNow}.<p> This content appears inside the element where hx-target attribute appears.</p>"""),
        "tag-selector" => Results.Content($"""Hello {DateTime.UtcNow}.<p> This content appears inside the article tag.</p>"""),
        "id-selector" => Results.Content($"""Hello {DateTime.UtcNow}.<p> This content appears inside a div with the matching id.</p>"""),
        "class-selector" => Results.Content($"""Hello {DateTime.UtcNow}.<p> This content appears inside a div with the matching class.</p>"""),
        _ => Results.Content("")
    };
});

app.Run();


