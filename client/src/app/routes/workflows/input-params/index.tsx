import { List, LookupField, Repeater, Tab, TextField } from "cx/widgets";
import Controller from "./Controller";
import { Store } from "cx/data";
import { LabelsTopLayout, bind, computable } from "cx/ui";

export default () => (
    <cx>
        <div controller={Controller} className="align-top">
            <div styles="margin:10px">
                <span className="p-2">Input parameter bindings:</span>
                <div className="flex flex-1 mt-4" styles="padding-left:10px;white-space:nowrap;">
                    <div className="overflow-x-auto w-30">
                        <Repeater records={bind("$task.inputs")} indexAlias="$index">
                            <Tab
                                text-bind="$record.tab"
                                tab-bind="$record.tab"
                                value-bind="$task.selectedInputTab"
                                default-expr="{$index} == 0 ? true : false"
                                mod="classic"
                            />
                        </Repeater>
                    </div>
                </div>
                <div className="flex flex-1" styles="border: 1px solid lightgray; background: white; padding: 20px">
                    <Repeater records={bind("$task.inputs")} recordAlias="$con" indexAlias="$conIndex">
                        <div
                            visible-expr="{$task.selectedInputTab}=={$con.tab}"
                            className="flex flex-1 flex-col items-center justify-middle"
                        >
                            <div className="flex flex-1" layout={LabelsTopLayout}>
                                <LookupField
                                    label="Source"
                                    options-bind="$con.source"
                                    value-bind="$con.sourceDecision"
                                    className="!w-full"
                                    if-expr="{$page.currentWorkflowInUndoneList} == true"
                                />
                                <TextField
                                    readOnly
                                    label="Source"
                                    className="!w-full"
                                    value-bind="$con.source"
                                    if-expr="{$page.currentWorkflowInUndoneList} == false"
                                />
                            </div>
                            <div className="flex flex-1" layout={LabelsTopLayout}>
                                <LookupField
                                    label="Param"
                                    options={computable("$con.sourceDecision", "$con.source", (id, sources) => {
                                        return id !== null && sources.length > id ? sources[id].param : [];
                                    })}
                                    value-bind="$con.paramDecision"
                                    className="!w-full"
                                    if-expr="{$page.currentWorkflowInUndoneList} == true"
                                />
                                <TextField
                                    readOnly
                                    label="Param"
                                    className="!w-full"
                                    value-bind="$con.param"
                                    if-expr="{$page.currentWorkflowInUndoneList} == false"
                                />
                            </div>
                        </div>
                    </Repeater>
                </div>
            </div>
        </div>
    </cx>
);
