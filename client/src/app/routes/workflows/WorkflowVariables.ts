import { Controller } from "cx/ui";

export default class extends Controller {
    static taskTypes: any = [{ id: 0, text: "SIMPLE" }];
    static FIXED_CHARS: string = "$.";
    static DEFAULT_SIGN: string = "$";

    static BACKEND_REQUEST_PLAY_WORKFLOW: string = "/workflows/playworkflow";
    static BACKEND_REQUEST_GET_ALL_TASKS: string = "/workflows/getalltasks";

    static IGNORE_OUTPUTKEYS: string = "<INSERT_REF_NAME>.output";

    static BACKEND_REQUEST_REGISTER_WORKFLOW: string = "/workflows";
}
