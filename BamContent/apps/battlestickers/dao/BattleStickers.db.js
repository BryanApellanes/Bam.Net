/*
	Copyright © Bryan Apellanes 2015  
*/
var database = {
    nameSpace: "Bam.Net.BattleStickers",
    schemaName: "BattleStickers",
    xrefs: [
        ["Player", "Character"],
        ["Player", "Weapon"],
        ["Player", "Spell"],
        ["Player", "Skill"],
        ["Player", "Equipment"],
        ["PlayerOne", "Character"],
        ["PlayerOne", "Weapon"],
        ["PlayerOne", "Spell"],
        ["PlayerOne", "Skill"],
        ["PlayerOne", "Equipment"],
        ["PlayerTwo", "Character"],
        ["PlayerTwo", "Weapon"],
        ["PlayerTwo", "Spell"],
        ["PlayerTwo", "Skill"],
        ["PlayerTwo", "Equipment"],
        ["RequiredLevel", "Character"],
        ["RequiredLevel", "Weapon"],
        ["RequiredLevel", "Spell"],
        ["RequiredLevel", "Skill"]
    ],
    tables: [
        {
            name: "Battle",
            fks: [
                { RockPaperScissorsWinnerId: "Player" }
            ],
            cols: [
                { MaxActiveCharacters: "Int", Null: false }
            ]
        },
        {
            name: "Player",
            cols: [
                { Name: "String", Null: false },
                { Level: "Int", Null: false },
                { ExperiencePoints: "Int", Null: false }
            ]
        },
        {
            name: "PlayerOne",
            fks: [
                { BattleId: "Battle" },
                { PlayerId: "Player" }
            ]
        },
        {
            name: "PlayerTwo",
            fks: [
                { BattleId: "Battle" },
                { PlayerId: "Player" }
            ]
        },
        {
            name: "Character",
            cols: [
                { Name: "String", Null: false },
                { Strength: "Int", Null: false },
                { Defense: "Int", Null: false },
                { Speed: "Int", Null: false },
                { Magic: "Int", Null: false },
                { Acuracy: "Int", Null: false },
                { Element: "String", Null: false },
                { MaxHealth: "Int", Null: false }
            ]
        },
        {
            name: "RequiredLevel",
            cols: [
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "PlayerTwoCharacterHealth",
            fks: [
                { PlayerTwoId: "PlayerTwo" },
                { CharacterId: "Character" }
            ],
            cols: [
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "PlayerOneCharacterHealth",
            fks: [
                { PlayerOneId: "PlayerOne" },
                { CharacterId: "Character" }
            ],
            cols: [
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "EffectOverTime",
            cols: [
                { Name: "String", Null: false },
                { Strength: "Int", Null: false },
                { Speed: "Int", Null: false }
            ]
        },
        {
            name: "Weapon",
            fks: [
                { EffectOverTimeId: "EffectOverTime" },
            ],
            cols: [
                { Name: "String", Null: false },
                { Strength: "Int", Null: false },
                { Element: "String", Null: false }
            ]
        },
        {
            name: "Spell",
            fks: [
                { EffectOverTimeId: "EffectOverTime" },
            ],
            cols: [
                { Name: "String", Null: false },
                { Strength: "Int", Null: false },
                { Element: "String", Null: false }
            ]
        },
        {
            name: "Skill",
            fks: [
                { EffectOverTimeId: "EffectOverTime" },
            ],
            cols: [
                { Name: "String", Null: false },
                { Strength: "Int", Null: false },
                { Element: "String", Null: false }
            ]
        },
        {
            name: "Equipment",
            cols: [
                { Name: "String", Null: false },
                { Element: "String", Null: false }
            ]
        },
        {
            name: "Effect",
            fks: [
                { EquipmentId: "Equipment" }
            ],
            cols: [
                { Attribute: "String", Null: false },
                { Value: "Int", Null: false }
            ]
        }
    ]
};
