var builder = DistributedApplication.CreateBuilder(args);

var identity = builder.AddProject<Projects.MultiShop_IdentityServer>("identity");

var basket = builder.AddProject<Projects.MultiShop_Basket>("basket");
var catalog = builder.AddProject<Projects.MultiShop_Catalog>("catalog");
var discount = builder.AddProject<Projects.MultiShop_Discount>("discount");
var comment = builder.AddProject<Projects.MultiShop_Comment>("comment");
var payment = builder.AddProject<Projects.MultiShop_Payment>("payment");
var message = builder.AddProject<Projects.MultiShop_Message>("message");
var images = builder.AddProject<Projects.MulltiShop_Images>("images");
var order = builder.AddProject<Projects.MultiShop_Order_WebApi>("order");
var cargo = builder.AddProject<Projects.MultiShop_Cargo_WebApi>("cargo");

var gateway = builder.AddProject<Projects.MultiShop_OcelotGateway>("gateway")
    .WithReference(identity)
    .WithReference(catalog)
    .WithReference(discount)
    .WithReference(order)
    .WithReference(cargo)
    .WithReference(basket)
    .WithReference(comment)
    .WithReference(payment)
    .WithReference(images)
    .WithReference(message);

builder.Build().Run();
