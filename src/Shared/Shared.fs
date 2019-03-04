namespace Shared
open System.Collections.Generic

type Counter = { Value : int }

type ContentLine = { content : string; parsedContent : string }
type Document = { Lines : ContentLine list }