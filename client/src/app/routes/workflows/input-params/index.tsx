import { List, LookupField, Repeater, Tab } from "cx/widgets";
import Controller from "./Controller";
import { Store } from "cx/data";
import { LabelsTopLayout, bind } from "cx/ui";

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
                                default-expr="{$index} == 0? true : false"
                                mod="classic"
                            />
                        </Repeater>
                    </div>
                </div>
                <div className="flex flex-1" styles="border: 1px solid lightgray; background: white; padding: 20px">
                    <Repeater records={bind("$task.inputs")} recordAlias="$con">
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
                                />
                            </div>
                            <div className="flex flex-1" layout={LabelsTopLayout}>
                                <LookupField label="Param" options-bind="$con.param" value-bind="$con.paramDecision" className="!w-full" />
                            </div>
                        </div>
                    </Repeater>
                </div>
            </div>
        </div>
    </cx>
);
