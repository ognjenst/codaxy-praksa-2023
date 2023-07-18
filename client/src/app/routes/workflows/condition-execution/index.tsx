import { LookupField, Repeater, Tab, TextField } from "cx/widgets";
import Controller from "./Controller";
import { LabelsTopLayout, bind } from "cx/ui";

export default () => (
    <cx>
        <div class="w-full mt-3 bg-white border-2 border-gray-700 col-span-4 rounded-sm">
            <div className="relative">
                <span className="relative -top-4 left-5 bg-white p-2" text="Condition for execution" />
            </div>

            <div className="p-10 grid grid-rows-2" controller={Controller}>
                <div>
                    <div className="flex flex-1 mt-4" styles="padding-left:10px;white-space:nowrap;">
                        <Repeater records={bind("$page.condition.arr")} indexAlias="$index">
                            <Tab
                                text-bind="$record.tab"
                                tab-bind="$record.tab"
                                value-bind="$record.selecteTab"
                                default-expr="{$index} == 0"
                            />
                        </Repeater>
                    </div>
                    <div className="flex flex-1" styles="border: 1px solid lightgray; background: white; padding: 20px">
                        <Repeater records={bind("$page.condition.arr")}>
                            <div
                                visible-expr="{$record.selecteTab} == {$record.tab}"
                                className="flex flex-1 lg:flex-row md:flex-col sm:flex-col"
                            >
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
                <div>
                    <TextField
                        className="w-full"
                        label="Expression: "
                        value-bind="$page.condition.expression"
                        required
                        placeholder="$.Input1 > 27 && $.Input2 < 20ms"
                    />
                </div>
            </div>
        </div>
    </cx>
);

/*

$page.condition.expression
$page.condition.arr

*/
