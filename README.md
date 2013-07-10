EmberTemplateBundles
====================

A library for precompilation and bundeling of ember handlebars templates based on the ASP.NET bundeling technology


Add a `EmberTemplatesTransform` to your templates bundle:

    var emberTemplatesTransform = new EmberTemplatesTransform("~/Scripts/handlebars-1.0.0.js", "~/Scripts/ember-1.0.0.js");
    bundles.Add(new Bundle("~/bundles/templates", emberTemplatesTransform)
        .IncludeDirectory("~/Scripts/app/templates/", "*.hbs", true)
        );

The transform will need a reference to handlebars.js and ember.js to be able to precompile the templates.

The templates can be rendered with the `EmberTemplates` helper:

        @Scripts.Render("~/bundles/...")
        ...
        @EmberTemplates.Render("~/bundles/templates")
    <body>
    ...

Rembember to add `@using EmberTemplateBundles` in the view or add `EmberTemplateBundles`to `<pages><namespaces>` in web.config

In a development scenario set `BundleTables.EnableOptimizations = true;` and the templates will be rendered inline.


The templates' name will be decided based on their location relative to `EmberTemplatesTransform`s `TemplatesRoot`
(by default: "~/Scripts/App/Templates"). Any template by the name of "default" will inherit the name of its parent folder

Examples:

* (TemplatesRoot)\application.hbs     -> "application"
* (TemplatesRoot)\posts\index.hbs     -> "posts/index"
* (TemplatesRoot)\posts\default.hbs   -> "posts"
* (TemplatesRoot)\posts.hbs           -> "posts"





This project is heavily inspired by [Mysliks](https://github.com/Myslik) [csharp-ember-handlebars](https://github.com/Myslik/csharp-ember-handlebars), 
