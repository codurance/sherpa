using MongoDB.Bson;
using MongoDB.Driver;

namespace SherpaBackEnd.Tests.Helpers.Analysis.Infrastructure;

public class MongoAnalysisHelper
{
    public static async Task InsertTemplate(IMongoCollection<BsonDocument> templateCollection)
    {
        await templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", "Hackman Model" },
            {
                "questions", new BsonArray()
                {
                    new BsonDocument()
                    {
                        { "component", "Real Team" },
                        { "position", "1" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        },
                    },
                    new BsonDocument()
                    {
                        { "component", "Enabling Structure" },
                        { "position", "2" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        },
                    },
                    new BsonDocument()
                    {
                        { "component", "Enabling Structure" },
                        { "position", "3" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        }
                    },
                    new BsonDocument()
                    {
                        { "component", "Compelling Direction" },
                        { "position", "4" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        }
                    },
                    new BsonDocument()
                    {
                        { "component", "Expert Coaching" },
                        { "position", "5" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        }
                    },
                    new BsonDocument()
                    {
                        { "component", "Supportive Coaching" },
                        { "position", "6" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        }
                    }
                }
            },
            { "minutesToComplete", 30 }
        });
    }

    public static async Task InsertSurveyWithTeamId(IMongoCollection<BsonDocument> surveyCollection, Guid teamId)
    {
        await surveyCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", "8caba1b3-c931-4b98-95c9-58ebac0045db" },
            { "Title", "Super Survey" },
            { "Status", 0 },
            { "Deadline", new DateTime() },
            { "Description", "Sample description" },
            { "Team", teamId.ToString() },
            { "Template", "Hackman Model" },
            {
                "Responses", new BsonArray()
                {
                    new BsonDocument()
                    {
                        { "TeamMemberId", "8a5f4cce-018a-4a6c-8901-44b729973c1d" },
                        {
                            "Answers", new BsonArray()
                            {
                                new BsonDocument
                                {
                                    { "Number", 1 },
                                    { "Answer", "1" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 2 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 3 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 4 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 5 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 6 },
                                    { "Answer", "5" }
                                },
                            }
                        }
                    },
                    new BsonDocument()
                    {
                        { "TeamMemberId", "8a5f4cce-018a-4a6c-8901-432121212bb1" },
                        {
                            "Answers", new BsonArray()
                            {
                                new BsonDocument
                                {
                                    { "Number", 1 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 2 },
                                    { "Answer", "1" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 3 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 4 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 5 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 6 },
                                    { "Answer", "5" }
                                }
                            }
                        }
                    }
                }
            },
            {
                "Coach", new BsonDocument
                {
                    { "_id", "92fb4bb7-ef6a-44b4-b48c-d5c751d6d22d" },
                    { "Name", "Lucia" }
                }
            }
        });
    }
}