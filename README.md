# HTML Helper Extensions

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](paypal.me/leandroberti)

To make it easier to add content to a view, you can take advantage of something called an HTML Helper.
An HTML Helper, typically, is a method that generates a string.
You can use HTML Helpers to generate standard HTML elements such as textboxes, links, dropdown lists, and list boxes.

All of the HTML Helpers methods are called on the **Html property** of the view.
For example, you render a TextBox by calling the ```Html.TextBox()``` method.

Using HTML Helper methods is optional.
They make your life easier by reducing the amount of HTML and script that you need to write.


## How it works?

The ASP.NET MVC framework includes a useful utility class named the **TagBuilder** class that you can use when building HTML helpers.
The TagBuilder class, as the name of the class suggests, enables you to easily build **HTML tags**.

This extension provides two new HTML helpers:

* ```Html.Button()``` to render an HTML button element with an _Icon Font_ inside ```<button><i class="fa fa-car"></i></button>```
* ```Html.LinkButton()``` to render an HTML link element (using the ```<a href="/"></a>``` tag) with an _Icon Font_ inside ```<a href="#" class="btn btn-default"><i class="fa fa-car"></i></a>``` that will be rendered like a **button**.

## How to use this extension?

Inside our razor view (```.cshtml``` file), we only need to use the HTML Helper and pass the required parameters.

**Rendering a button**

We use the following helper extension:

```C#
@Html.Button("btnCreate", "Create New", Url.Action("Create", "Area"), true, new { @class = "btn btn-info mrg-l-r-5" }, new { @class = "fa fa-file-text-o" })
```

That will generate this button
![alt text](https://raw.githubusercontent.com/leandroberti/HtmlHelperExtensions/master/Images/Button.png "Rendered Button")
with the HTML tag:
```C#
<button class="btn btn-info mrg-l-r-5" id="btnCreate" name="btnCreate" onclick="location.href='/PortalTalentosInternos/Area/Create'">
    <i class="fa fa-file-text-o"></i> Create New
</button>
```

**Rendering a Link Button**

We use the following helper extension:

```C#
@Html.LinkButton("btnEdit", "Edit", @Url.Action("Edit", "Area", new { Model.Id }), new { @class = "btn btn-success mrg-l-r-5" }, new { @class = "fa fa-edit" })
```

That will generate this link button
![alt text](https://raw.githubusercontent.com/leandroberti/HtmlHelperExtensions/master/Images/LinkButton.png "Rendered Link Button")
with the HTML tag:
```C#
<a class="btn btn btn-success mrg-l-r-5" href="/PortalTalentosInternos/Area/Edit/0" id="btnEdit" name="btnEdit">
    <i class="fa fa-edit"></i> Edit
</a>
```

## Where to get this extension?

You can install this extension direct from:

[![Nuget](https://img.shields.io/badge/nuget-v1.0.1-blue.svg)](https://www.nuget.org/packages/LMB.HtmlHelperExtensions/)

Or instal with Package Manager Console

```C#
Install-Package LMB.HtmlHelperExtensions
```

Or you can download this github project and copy the `HtmlHelperExtensions.cs` file direct into your project.

# Donations

**If you enjoy this work, please consider supporting me for developing and maintaining this (and others) templates.**

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=26TY9QLTDWDSE&lc=US&item_name=leandroberti&item_number=github&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHosted)
