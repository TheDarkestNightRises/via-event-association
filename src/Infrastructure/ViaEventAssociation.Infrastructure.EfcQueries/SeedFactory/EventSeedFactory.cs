﻿using System.Text.Json;

namespace ViaEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public class EventSeedFactory
{
    private const string EventsAsJson = """
                                        [
                                          {
                                            "Id": "40ed2fd9-2240-4791-895f-b9da1a1f64e4",
                                            "Title": "Friday Bar",
                                            "Description": "Come for the cheap beer and great company.",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-01 15:00",
                                            "End": "2024-03-01 21:00",
                                            "MaxGuests": 50,
                                            "LocationId": "731cea3a-4e31-4af5-b537-7b35c3dbe73c"
                                          },
                                          {
                                            "Id": "95ebf41a-0b91-4a98-a68b-c4fe095dc0ba",
                                            "Title": "Scary Movie Night",
                                            "Description": "Come watch some of the classic horror movies. Bring your own popcorn.",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-06 17:00",
                                            "End": "2024-03-07 00:00",
                                            "MaxGuests": 50,
                                            "LocationId": "efd18631-e050-4bd0-a15a-6d5a17cca490"
                                          },
                                          {
                                            "Id": "40d73623-48d4-4862-b116-7ee7cdfda22f",
                                            "Title": "Friday Bar The Second",
                                            "Description": "Free watery beer the first hour!",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-08 15:00",
                                            "End": "2024-03-08 21:00",
                                            "MaxGuests": 50,
                                            "LocationId": "731cea3a-4e31-4af5-b537-7b35c3dbe73c"
                                          },
                                          {
                                            "Id": "d1755912-7920-44ac-b2e0-93a18e388cda",
                                            "Title": "Orienteering",
                                            "Description": "Come join us for orienteering run all over campus, there will be stairs. Many stairs.",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-11 17:00",
                                            "End": "2024-03-11 19:00",
                                            "MaxGuests": 50,
                                            "LocationId": "e0b61b58-0af5-4d64-b801-27153bdf1c01"
                                          },
                                          {
                                            "Id": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                            "Title": "Pizza Party",
                                            "Description": "Share a slice with your co-students, meet new friends. Be quick to accept invitations!",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-03-14 17:00",
                                            "End": "2024-03-14 21:00",
                                            "MaxGuests": 30,
                                            "LocationId": "e4c821b2-2b98-49f6-b533-49ac8efd5d00"
                                          },
                                          {
                                            "Id": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                            "Title": "Chess and beer",
                                            "Description": "Interested in chess? Come by, and learn the game, play with new friends. Have a beer",
                                            "Status": "ready",
                                            "Visibility": "private",
                                            "Start": "2024-03-15 16:00",
                                            "End": "2024-03-15 20:00",
                                            "MaxGuests": 20,
                                            "LocationId": "cc395a25-5b53-41de-a5bf-258c90a5bc43"
                                          },
                                          {
                                            "Id": "5ae1f5a0-ab65-4c10-bc1e-ff7ec590b385",
                                            "Title": "DnD introductions!",
                                            "Description": "Get an introduction to the magical world of Dungeons and Dragons. Bring your best LARPing outfit!",
                                            "Status": "draft",
                                            "Visibility": "private",
                                            "Start": "2024-03-18 18:00",
                                            "End": "2024-03-18 23:00",
                                            "MaxGuests": 15,
                                            "LocationId": "cc395a25-5b53-41de-a5bf-258c90a5bc43"
                                          },
                                          {
                                            "Id": "78f3ae84-f979-4599-a32b-45d51ec1598c",
                                            "Title": "Board game Cafe",
                                            "Description": "We bring the boardgames and the pretzels, you bring your best mood, and your friends",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-20 14:00",
                                            "End": "2024-03-20 20:00",
                                            "MaxGuests": 50,
                                            "LocationId": "731cea3a-4e31-4af5-b537-7b35c3dbe73c"
                                          },
                                          {
                                            "Id": "d339a7da-fc10-4391-9426-30bbae8c79be",
                                            "Title": "Learn to knit!",
                                            "Description": "Want to create your own sweaters? This is it!",
                                            "Status": "cancelled",
                                            "Visibility": "private",
                                            "Start": "2024-03-21 15:00",
                                            "End": "2024-03-21 18:00",
                                            "MaxGuests": 20,
                                            "LocationId": "cc395a25-5b53-41de-a5bf-258c90a5bc43"
                                          },
                                          {
                                            "Id": "280baa82-5ac6-47d3-af05-291eb8eac9d3",
                                            "Title": "Whiskey Tasting",
                                            "Description": "Expand your taste buds, explore the world of whiskey. We are joined by local whiskey sommelier, Whilley Whiskeyson, and he brings the good stuff!",
                                            "Status": "draft",
                                            "Visibility": "private",
                                            "Start": "2024-03-23 19:00",
                                            "End": "2024-03-23 22:00",
                                            "MaxGuests": 15,
                                            "LocationId": "cc395a25-5b53-41de-a5bf-258c90a5bc43"
                                          },
                                          {
                                            "Id": "be01e864-b6c5-4ae7-b5b2-edc44529a842",
                                            "Title": "Easter Egg Hunt",
                                            "Description": "Join us for the yearly hunt for the easter eggs around campus. Bring your kids",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-25 15:00",
                                            "End": "2024-03-25 17:00",
                                            "MaxGuests": 30,
                                            "LocationId": "e0b61b58-0af5-4d64-b801-27153bdf1c01"
                                          },
                                          {
                                            "Id": "32c63c1c-8d02-4845-b50c-908fcc799bbf",
                                            "Title": "Movie night",
                                            "Description": "This time we go for the romantics. Meet new friends, share their popcorn, hold their hand. With their consent, of course.",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-03-26 18:00",
                                            "End": "2024-03-27 00:30",
                                            "MaxGuests": 30,
                                            "LocationId": "e4c821b2-2b98-49f6-b533-49ac8efd5d00"
                                          },
                                          {
                                            "Id": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                            "Title": "Speed dating",
                                            "Description": "Meet your next soul mate! We will provide a romantic setting",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-03-28 16:00",
                                            "End": "2024-03-28 20:00",
                                            "MaxGuests": 50,
                                            "LocationId": "def55e29-c806-4cc1-a0f5-91757446212a"
                                          },
                                            {
                                            "Id": "d127e2fd-7da7-4788-ab8b-c9dfbd04964f",
                                            "Title": "Beer Tasting!",
                                            "Description": "Come taste the greatest beer from the greatest micro brews around Horsens. Are you a micro-brewer yourself? Bring your own stuff for the joy of others!",
                                            "Status": "ready",
                                            "Visibility": "public",
                                            "Start": "2024-04-02 15:00",
                                            "End": "2024-04-02 18:00",
                                            "MaxGuests": 50,
                                            "LocationId": "def55e29-c806-4cc1-a0f5-91757446212a"
                                          },
                                          {
                                            "Id": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                            "Title": "Train Spotting. Choo Choo!",
                                            "Description": "Are you also interesting in trains? Trains are great. And we have a train yard just next to VIA! We meet at the atriet, and go together to watch the trains zoom by.",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-04-03 16:00",
                                            "End": "2024-04-03 19:00",
                                            "MaxGuests": 20,
                                            "LocationId": "da6c8bde-d7d6-4921-9512-ae469d6a3f4e"
                                          },
                                          {
                                            "Id": "23a28a9a-2380-468d-9afc-c5cc1cda66f5",
                                            "Title": "Garden Games",
                                            "Description": "Join us for a fun afternoon with all the classic garden games. We have stuff like Molkky, Axe throwing, Bean Bag Throwing, Ladder Golf, and much much more.",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-04-05 14:00",
                                            "End": "2024-04-05 18:00",
                                            "MaxGuests": 50,
                                            "LocationId": "da6c8bde-d7d6-4921-9512-ae469d6a3f4e"
                                          },
                                          {
                                            "Id": "d3b313b8-33bc-42e7-8d88-f49a7a336b0d",
                                            "Title": "Card Stacking.",
                                            "Description": "Would you like to stack cards? Real high? But are you lacking the skills? Come today, learn all the tips and tricks from the pros.",
                                            "Status": "draft",
                                            "Visibility": "private",
                                            "Start": "2024-04-08 19:00",
                                            "End": "2024-04-08 21:00",
                                            "MaxGuests": 20,
                                            "LocationId": "cc395a25-5b53-41de-a5bf-258c90a5bc43"
                                          },
                                          {
                                            "Id": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                            "Title": "Yoga, introduction level",
                                            "Description": "Is your back hurting from being hunched over the keyboard all day? Join us for some yoga, to stretch out that crumbled body of yours.",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-04-09 16:00",
                                            "End": "2024-04-09 17:30",
                                            "MaxGuests": 20,
                                            "LocationId": "def55e29-c806-4cc1-a0f5-91757446212a"
                                          },
                                          {
                                            "Id": "2d8d4384-6058-46ae-a821-253e0928a631",
                                            "Title": "Art Exhibition",
                                            "Description": "Local artist in Horsens will join us and show off their art work.",
                                            "Status": "ready",
                                            "Visibility": "public",
                                            "Start": "2024-04-11 15:00",
                                            "End": "2024-04-11 18:00",
                                            "MaxGuests": 50,
                                            "LocationId": "def55e29-c806-4cc1-a0f5-91757446212a"
                                          },
                                          {
                                            "Id": "1a8dd0b1-8037-460b-81b9-849380e15cba",
                                            "Title": "Cloud Watching",
                                            "Description": "Need a bit of chill time? Come watch some clouds, see what shapes you can find. Bring a blanket to lie down on.",
                                            "Status": "active",
                                            "Visibility": "public",
                                            "Start": "2024-04-12 12:00",
                                            "End": "2024-04-12 13:00",
                                            "MaxGuests": 50,
                                            "LocationId": "da6c8bde-d7d6-4921-9512-ae469d6a3f4e"
                                          },
                                          {
                                            "Id": "2e8b9bc4-f83b-4b20-9b3f-022bfcea56c0",
                                            "Title": "Origami Introduction",
                                            "Description": "Want to fold paper? In a nice way? So you have something to do in class, when you\u0027re bored? Impress your friends by folding a nice paper plane. Local origami master Oleg provides the tricks of the trade.",
                                            "Status": "cancelled",
                                            "Visibility": "private",
                                            "Start": "2024-04-15 16:00",
                                            "End": "2024-04-15 18:00",
                                            "MaxGuests": 20,
                                            "LocationId": "cc395a25-5b53-41de-a5bf-258c90a5bc43"
                                          },
                                          {
                                            "Id": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                            "Title": "Cheese rolling",
                                            "Description": "Ever rolled a cheese before? Want to try it? Competitively? We have the chess, you come and do the rolling. Limited number of contestents, but spectators are welcome!",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-04-17 15:00",
                                            "End": "2024-04-17 17:00",
                                            "MaxGuests": 20,
                                            "LocationId": "da6c8bde-d7d6-4921-9512-ae469d6a3f4e"
                                          },
                                          {
                                            "Id": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                            "Title": "Poker night",
                                            "Description": "Come and loose all your hard-earned SU, while having a great time. We play Texas Hold\u0027em",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-04-19 18:00",
                                            "End": "2024-04-20 00:30",
                                            "MaxGuests": 30,
                                            "LocationId": "def55e29-c806-4cc1-a0f5-91757446212a"
                                          },
                                          {
                                            "Id": "aea22827-07ba-4379-a215-c595020af476",
                                            "Title": "Juggling",
                                            "Description": "Want to learn to juggle? Everyone wants to learn. Come and have fun.",
                                            "Status": "ready",
                                            "Visibility": "public",
                                            "Start": "2024-04-22 15:00",
                                            "End": "2024-04-22 17:00",
                                            "MaxGuests": 50,
                                            "LocationId": "eb34cf58-a348-4400-8ace-2ad775912db7"
                                          },
                                          {
                                            "Id": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                            "Title": "Floorball tournament",
                                            "Description": "Which study programme is the best at floorball? Grab some friends, we play teams of four, and join us for the ultimate showdown",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-04-25 14:00",
                                            "End": "2024-04-25 18:00",
                                            "MaxGuests": 30,
                                            "LocationId": "eb34cf58-a348-4400-8ace-2ad775912db7"
                                          },
                                          {
                                            "Id": "9f7d4bc6-1595-4fa9-bd35-63d1e771a10f",
                                            "Title": "Soap Carving",
                                            "Description": "This medidative exercise is really great, if you\u0027re stressed out from having too much homework. So, instead of doing that homework, come carve some soap. It\u0027s relaxing.",
                                            "Status": "draft",
                                            "Visibility": "public",
                                            "Start": "2024-04-26 15:00",
                                            "End": "2024-04-26 18:00",
                                            "MaxGuests": 50,
                                            "LocationId": "731cea3a-4e31-4af5-b537-7b35c3dbe73c"
                                          },
                                          {
                                            "Id": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                            "Title": "Lan Party",
                                            "Description": "Come play Counter Strike, like the good old days! We provide redbull and pretzels",
                                            "Status": "active",
                                            "Visibility": "private",
                                            "Start": "2024-04-29 14:00",
                                            "End": "2024-04-30 00:00",
                                            "MaxGuests": 30,
                                            "LocationId": "e4c821b2-2b98-49f6-b533-49ac8efd5d00"
                                          },
                                          {
                                            "Id": "9b42358b-680f-415b-838a-a4d329f68afe",
                                            "Title": "Extreme Ironing",
                                            "Description": "Are your shirts always wringled? Bring them all for this event of competitive ironing. There will be prizes.",
                                            "Status": "draft",
                                            "Visibility": "public",
                                            "Start": "2024-04-30 15:00",
                                            "End": "2024-04-30 19:00",
                                            "MaxGuests": 50,
                                            "LocationId": "def55e29-c806-4cc1-a0f5-91757446212a"
                                          }
                                        ]
                                        """;
    
    public static List<Event> CreateEvents()
    {
        List<TmpEvent> eventsTmp = JsonSerializer.Deserialize<List<TmpEvent>>(EventsAsJson)!;

        var events = eventsTmp.Select(e => new Event
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Status = e.Status,
            Visibility = e.Visibility,
            Start = e.Start,
            End = e.End,
            Capacity = e.MaxGuests
        }).ToList();

        return events;
    }


    public record TmpEvent(string Id, string Title, string Description, string Status, string Visibility, string? Start, string? End, int MaxGuests, string LocationId);
}