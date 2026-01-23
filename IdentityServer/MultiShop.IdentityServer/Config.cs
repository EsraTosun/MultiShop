using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MultiShop.IdentityServer;

public static class Config
{
    // ================= IDENTITY =================
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    // ================= API SCOPES =================
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("Catalog.Full"),
            new("Catalog.Read"),
            new("Discount.Full"),
            new("Order.Full"),
            new("Cargo.Full"),
            new("Basket.Full"),
            new("Comment.Full"),
            new("Payment.Full"),
            new("Image.Full"),
            new("Message.Full"),
            new("Ocelot.Full"),
            new(IdentityServerConstants.LocalApi.ScopeName)
        };

    // ================= API RESOURCES =================
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new("catalog.api"){ Scopes = { "Catalog.Full", "Catalog.Read" }},
            new("discount.api"){ Scopes = { "Discount.Full" }},
            new("order.api"){ Scopes = { "Order.Full" }},
            new("cargo.api"){ Scopes = { "Cargo.Full" }},
            new("basket.api"){ Scopes = { "Basket.Full" }},
            new("comment.api"){ Scopes = { "Comment.Full" }},
            new("payment.api"){ Scopes = { "Payment.Full" }},
            new("image.api"){ Scopes = { "Image.Full" }},
            new("message.api"){ Scopes = { "Message.Full" }},
            new("ocelot.api"){ Scopes = { "Ocelot.Full" }},
            new(IdentityServerConstants.LocalApi.ScopeName)
        };

    // ================= CLIENTS =================
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // VISITOR (M2M)
            new Client
            {
                ClientId = "multishop.visitor",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("visitor-secret".Sha256()) },
                AllowedScopes =
                {
                    "Catalog.Read",
                    "Image.Full",
                    "Comment.Full",
                    "Ocelot.Full"
                }
            },

            // MANAGER (LOGIN)
            new Client
            {
                ClientId = "multishop.manager",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("manager-secret".Sha256()) },

                AllowedScopes =
                {
                    "Catalog.Full",
                    "Order.Full",
                    "Basket.Full",
                    "Discount.Full",
                    "Payment.Full",
                    "Message.Full",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.LocalApi.ScopeName
                }
            },

            // ADMIN
            new Client
            {
                ClientId = "multishop.admin",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("admin-secret".Sha256()) },
                AccessTokenLifetime = 600,

                AllowedScopes =
                {
                    "Catalog.Full",
                    "Order.Full",
                    "Cargo.Full",
                    "Basket.Full",
                    "Discount.Full",
                    "Payment.Full",
                    "Message.Full",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.LocalApi.ScopeName
                }
            }
        };
}
