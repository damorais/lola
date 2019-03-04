module Client
    open Elmish
    open Elmish.React

    open Fable.Helpers.React.Props
    open Fable.PowerPack.Fetch
    open Thoth.Json
    open Shared

    open Fable.Helpers.React

    type Model = {
        Document : Document
        CurrentLine : int
    }

    let newModel = {
        Document = { Lines = [
                { content =  "Isto é uma linha"; parsedContent = "Isto é uma linha" };
                { content =  "Isto é uma linha2"; parsedContent = "Isto é uma linha2" }
            ] };
        CurrentLine = 1
    }

    type Msg =
    | NewLine of string
    | RemoveLine of int
    | UpdateLine of string * int

    let init () : Model * Cmd<Msg> =
        newModel, Cmd.none

    let update (msg: Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match currentModel, msg with
        | currentModel, NewLine content ->
            let nextModel = { currentModel with Document = { Document.Lines = { content =  content; parsedContent = content } :: currentModel.Document.Lines  } }
            nextModel, Cmd.none
        | currentModel, RemoveLine index -> currentModel, Cmd.none
        | currentModel, UpdateLine (content, index) -> currentModel, Cmd.none


    let documentLine (line:ContentLine) =
        div [] [ str line.content ]

    let documentContent (document:Document) =
        div []
            (document.Lines |> List.map documentLine)

    let view (model: Model) (dispatch : Msg -> unit) =
        div []
            [
                documentContent model.Document
                input [
                    Id "txtNewLine"
                    Type "text"
                    OnChange (fun e -> dispatch (NewLine e.Value))
                ]
            ]

    #if DEBUG
    open Elmish.Debug
    open Elmish.HMR
    #endif

    Program.mkProgram init update view
    #if DEBUG
    |> Program.withConsoleTrace
    |> Program.withHMR
    #endif
    |> Program.withReact "elmish-app"
    #if DEBUG
    |> Program.withDebugger
    #endif
    |> Program.run