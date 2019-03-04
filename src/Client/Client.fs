module Client
    open Elmish
    open Elmish.React

    open Fable.Helpers.React.Props
    open Fable.PowerPack.Fetch
    open Thoth.Json
    open Shared

    open Fable.Helpers.React

    // open Fulma

    // type Model = { Counter: Counter }

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

    // type Msg =
    // | Increment
    // | Decrement
    // | UpdateValue of int

    type Msg =
    | NewLine of string
    | RemoveLine of int
    | UpdateLine of string * int

    // let init () : Model * Cmd<Msg> =
    //     { Counter = { Value = 0 } } , Cmd.none

    let init () : Model * Cmd<Msg> =
        newModel, Cmd.none

    let update (msg: Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match currentModel, msg with
        | currentModel, NewLine content ->
            let nextModel = { currentModel with Document = { Document.Lines = { content =  content; parsedContent = content } :: currentModel.Document.Lines  } }
            nextModel, Cmd.none
        | currentModel, RemoveLine index -> currentModel, Cmd.none
        | currentModel, UpdateLine (content, index) -> currentModel, Cmd.none

    // let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    //     match currentModel.Counter, msg with
    //     | counter, Increment ->
    //         let nextModel = { currentModel with Counter = { Value = counter.Value + 1 } }
    //         nextModel, Cmd.none
    //     | counter, Decrement ->
    //         let nextModel = { currentModel with Counter = { Value = counter.Value - 1 } }
    //         nextModel, Cmd.none
    //     | _ , UpdateValue value ->
    //         let nextModel = { currentModel with Counter = { Value = value }}
    //         nextModel, Cmd.none


    // let view (model : Model) (dispatch : Msg -> unit ) =
    //     div []
    //         [
    //             str "Digite um número: "
    //             button [ OnClick (fun _ -> dispatch Decrement)] [ str "-" ]
    //             input [
    //                     Id "txtContador"
    //                     Type "text"
    //                    OnChangealue model.Counter.Value
    //                    OnChangenChange (fun e -> dispatch (UpdateValue (int e.Value)))
    //                   ]OnChange
    //             button [ OnClick (fun _ -> dispatch Increment)] [ str "+" ]
    //             str ("O valor atual é: " + (string model.Counter.Value ))
    //         ]


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