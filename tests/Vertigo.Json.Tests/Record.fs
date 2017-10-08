﻿namespace Vertigo.Json

module Record =
    open System
    open Vertigo.Json
    open NUnit.Framework

    type SimpleRecord = {
        intmember: int
        stringmember: string
    }

    [<Test>]
    let ``Record serialization to string`` () =
        let expected = """{"intmember":3,"stringmember":"text"}"""
        let value = { SimpleRecord.intmember = 3; stringmember = "text" }
        let json = Json.serializeU value
        Assert.AreEqual(expected, json)

    [<Test>]
    let ``Record serialization-deserialization`` () =
        let value = { SimpleRecord.intmember = 3; stringmember = "text" }
        let json = Json.serialize(value)
        let actual = Json.deserialize<SimpleRecord>(json)
        Assert.AreEqual(value, actual)

    type SimpleString = {
        amember: string
    }

    type Bigger = {
        nested: SimpleString
    }

    [<Test>]
    let ``Record nested serialization-deserialization`` () =
        let value = { Bigger.nested = { SimpleString.amember = "Some text" } }
        let s = Json.serialize(value)
        let actual = Json.deserialize<Bigger>(s)
        Assert.AreEqual(value, actual)