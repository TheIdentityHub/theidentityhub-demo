theidentityhub-demo - ASP.NET MVC
=====================================

The Identity Hub makes it easy to connect your app to all major identity providers like Microsoft, Facebook, Google, Twitter, Linked In and more. For more information visit [The Identity Hub](https://www.theidentityhub.com)

Getting Started
===============

Download or Clone the repository. 

In the following line of code

new IdentityService("[Your App Client Id]", "[Your Tenant]"));

1. Replace [Your App Client Id] with the Client Id from your App configured in The Identity Hub.
2. Replace [Your Tenant] with the url of your tenant on The Identity Hub.

In the web.config replace the system.identityModel and system.identityModel.services nodes with the configuration you can download from the App detail page on The Identity Hub. See https://identityhub.be/IdentityHub/hub/Documentation/#LoginAspNet

Run the App.

If you do not have already created an App see https://www.theidentityhub.com/hub/Documentation/#CreateAnApp.



