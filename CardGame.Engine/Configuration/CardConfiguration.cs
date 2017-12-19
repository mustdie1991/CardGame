using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Common.Classes;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;

namespace CardGame.Engine.Configuration
{
    public class CardConfiguration
    {
        public const string CountName = "count";

        public const string CostName = "cost";

        public const string Indices = "indices";

        private readonly List<Card> _cards;
        
        public CardConfiguration()
        {
            this._cards = new List<Card>
            {
                // TREASURE
                new TreasureCard {Name = "Copper", TreasureValue = 1, Cost = 0, CardName = CardNames.Copper},
                new TreasureCard {Name = "Silver", TreasureValue = 2, Cost = 3, CardName = CardNames.Silver},
                new TreasureCard {Name = "Gold", TreasureValue = 3, Cost = 6, CardName = CardNames.Gold},

                // VICTORY
                new VictoryCard {Name = "Province", VictoryPoints = 6, Cost = 0, CardName = CardNames.Province},
                new VictoryCard {Name = "Estate", VictoryPoints = 1, Cost = 2, CardName = CardNames.Estate},
                new VictoryCard {Name = "Duchy", VictoryPoints = 3, Cost = 5, CardName = CardNames.Duchy},
                new VictoryCard {Name = "Curse", VictoryPoints = -1, Cost = 0, CardName = CardNames.Curse},

                // ACTION
                new ActionCard
                {
                    Name = "Library",
                    Cost = 5,
                    CardName = CardNames.Library

                },

                //Witch(not full)
                new AttackCard
                {
                    Name = "Witch",
                    Cost = 5,
                    CardName = CardNames.Witch,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 2}
                            }
                        }
                    }
                },
                new AttackCard {Name = "Thief", Cost = 4, CardName = CardNames.Thief},

                //Council Room(not full)
                new ActionCard
                {
                    Name = "Council Room",
                    Cost = 5,
                    CardName = CardNames.CouncilRoom,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 4}
                            }
                        }
                    }
                },

                // Village
                new ActionCard
                {
                    Name = "Village",
                    Cost = 3,
                    CardName = CardNames.Village,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnMoves,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 2}
                            }
                        }
                    }
                },
                new ActionCard {Name = "Adventurer", Cost = 6, CardName = CardNames.Adventurer},
                new ActionCard {Name = "Chancellor", Cost = 3, CardName = CardNames.Chancellor},

                // Smithy
                new ActionCard
                {
                    Name = "Smithy",
                    Cost = 4,
                    CardName = CardNames.Smithy,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 3}
                            }
                        }
                    }
                },

                // Laboratory
                new ActionCard
                {
                    Name = "Laboratory",
                    Cost = 5,
                    CardName = CardNames.Laboratory,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 2}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnMoves,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        }
                    }
                },

                // Woodcutter
                new ActionCard
                {
                    Name = "Woodcutter",
                    Cost = 3,
                    CardName = CardNames.Woodcutter,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuyCoins,
                            Parameters = new Dictionary<string, object>
                            {
                                {CostName, 2}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuys,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        }
                    }
                },
                new ActionCard {Name = "Workshop", Cost = 3, CardName = CardNames.Workshop},

                // Militia(not full)
                new AttackCard
                {
                    Name = "Militia",
                    Cost = 4,
                    CardName = CardNames.Militia,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuyCoins,
                            Parameters = new Dictionary<string, object>
                            {
                                {CostName, 2}
                            }
                        }
                    }
                },
                new ActionCard {Name = "Cellar", Cost = 2, CardName = CardNames.Cellar},
                new ActionCard {Name = "Feast", Cost = 4, CardName = CardNames.Feast},

                // Market
                new ActionCard
                {
                    Name = "Market",
                    Cost = 5,
                    CardName = CardNames.Market,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuyCoins,
                            Parameters = new Dictionary<string, object>
                            {
                                {CostName, 1}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuys,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnMoves,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        }
                    }
                },
                new ActionCard {Name = "Remodel", Cost = 4, CardName = CardNames.Remodel},

                // Moat
                new ActionCard
                {
                    Name = "Moat",
                    Cost = 2,
                    CardName = CardNames.Moat,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnCards,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 2}
                            }
                        }
                    }
                },
                new ActionCard {Name = "Moneylender", Cost = 4, CardName = CardNames.Moneylender},
                new ActionCard {Name = "Mine", Cost = 5, CardName = CardNames.Mine},
                new ActionCard {Name = "ThroneRoom", Cost = 4, CardName = CardNames.ThroneRoom},
                new ActionCard {Name = "Chapel", Cost = 2, CardName = CardNames.Chapel},
                new ActionCard {Name = "Bureaucrat", Cost = 4, CardName = CardNames.Bureaucrat},
                new AttackCard {Name = "Spy", Cost = 4, CardName = CardNames.Spy},

                // Festival
                new ActionCard
                {
                    Name = "Festival",
                    Cost = 5,
                    CardName = CardNames.Festival,
                    CardActions =
                    {
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuyCoins,
                            Parameters = new Dictionary<string, object>
                            {
                                {CostName, 2}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnBuys,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 1}
                            }
                        },
                        new CardAction
                        {
                            CardActionName = CardActions.EarnMoves,
                            Parameters = new Dictionary<string, object>
                            {
                                {CountName, 2}
                            }
                        }
                    }
                },
                new ActionCard {Name = "Gardens", Cost = 4, CardName = CardNames.Gardens}
            };
        }

        public Card GetCardInfo(CardNames name)
        {
            return this._cards.FirstOrDefault(x => x.CardName == name);
        }

        public List<Card> GetCardsByPrice(int price)
        {
            return this._cards.Where(x => x.Cost <= price).ToList();
        }
    }
}
