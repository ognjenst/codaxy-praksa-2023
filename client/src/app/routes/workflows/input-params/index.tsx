import { List, LookupField, Repeater, Tab } from "cx/widgets";
import Controller from "./Controller";
import { Store } from "cx/data";
import { LabelsTopLayout, bind } from "cx/ui";

export default () => (
    <cx>
        <div controller={Controller}>
            <div styles="margin:10px">
                <span className="p-2">Input parameter bindings:</span>
                <div className="flex flex-1 mt-4" styles="padding-left:10px;white-space:nowrap;">
                    <Repeater records={bind("intro.core.inputBindings")}>
                        <Tab text-bind="$record.tab" tab-bind="$record.tab" value-bind="$page.tab" mod="classic" />
                    </Repeater>
                </div>
                <div className="flex flex-1" styles="border: 1px solid lightgray; background: white; padding: 20px">
                    <Repeater records={bind("intro.core.inputBindings")}>
                        <div visible-expr="{$page.tab}=={$record.tab}" className="flex flex-1 lg:flex-row md:flex-col sm:flex-col">
                            <div className="flex flex-1" layout={LabelsTopLayout}>
                                <LookupField label="Source" options-bind="$record.source" value-bind="$page.inputParam.decision1" />
                            </div>
                            <div className="flex flex-1 ml-0 md:ml-0 lg:ml-2" layout={LabelsTopLayout}>
                                <LookupField label="Param" options-bind="$record.param" value-bind="$page.inputParam.decision2" />
                            </div>
                        </div>
                    </Repeater>
                </div>
            </div>
        </div>
    </cx>
);
