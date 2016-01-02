var database = {
    nameSpace: "Bam.Net.Shop",
    schemaName: "Shop",
    xrefs: [
        ["ShoppingList", "ShopItem"],
        ["Shop", "ShopItem"],
        ["ShopItem", "ShopItemAttribute"],
        ["Shop", "Promotion"],
        ["ShopItem", "Promotion"]
    ],
    tables: [
        {
            name: "Currency",
            cols: [
                { Symbol: "String", Null: false },
                { Name: "String", Null: false }
            ]
        },
        {
            name: "CurrencyCountry",
            fks: [
                { CurrencyId: "Currency" }
            ],
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "Shop",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "PromotionEffects", 
            cols: [
                { Name: "String", Null: false } 
            ]
        },
        {
            name: "Promotion",
            cols: [
                { Name: "String", Null: false },
                { ValidFrom: "DateTime", Null: false },
                { ValidTo: "DateTime", Null: false }
            ]
        },
        {
            name: "PromotionEffect",
            fks: [
                { PromotionId: "Promotion" },
                { PromotionEffectsId: "PromotionEffects"}
            ],
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "PromotionCondition",
            fks: [
                { PromotionId: "Promotion" }                
            ],
            cols: [
                { Description: "String", Null: false },
                { Value: "Byte", Null: false } 
            ]
        },
        {
            name: "PromotionCode",
            fks: [
                { PromotionId: "Promotion" }
            ],
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Shopper",            
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ShoppingCart",
            fks: [
                { ShopperId: "Shopper" }
            ],
            cols: [
				{ Name: "String", Null: true }
            ]
        },
        {
            name: "ShoppingCartItem",
            fks:[
                { ShoppingCartId: "ShoppingCart" },
                { ShopItemId: "ShopItem" }
            ],
            cols: [
                { Quantity: "Int", Null: false }
            ]
        },
        {
            name: "ShoppingList",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ShopItem",
            fks: [
            ],
            cols: [
                { Name: "String", Null: false },                
                { Source: "String", Null: false },
                { SourceId: "String", Null: false },
                { DetailUrl: "String" },
                { ImageSrc: "String" }
            ]
        },
        {
            name: "Price",
            fks: [
               { ShopItemId: "ShopItem" },
               { CurrencyId: "Currency" }
            ],
            cols: [
                { Value: "Decimal", Null: false}
            ]
        },
        {
            name: "ShopItemAttribute",
            fks: [
            ],
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ShopItemAttributeValue",
            fks: [
                { ShopItemAttributeId: "ShopItemAttribute" }
            ],
            cols: [
                { Value: "String", Null: false }
            ]
        }
    ]
};
