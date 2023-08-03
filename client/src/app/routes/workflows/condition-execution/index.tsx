import { Button, LookupField, Repeater, Tab, ValidationGroup, TextField, HScroller, FlexCol, FlexRow } from "cx/widgets";
import Controller from "./Controller";
import { LabelsTopLayout, bind, computable } from "cx/ui";
import { openDryRunWindow } from "../dry-run";

export default () => (
    <cx>
        <div class="w-full mt-3 bg-white border border-gray-200 col-span-4 rounded-sm" controller={Controller}>
            <div className="flex">
                <div className="flex-1 m-2">
                    <span className="bg-white p-2 text-gray-600 whitespace-nowrap" text="Condition for execution" />
                </div>
                <div className="flex-1 grid justify-items-end mt-2 mr-2">
                    <Button icon="plus" className="rounded-full h-8 w-8 ml-2" onClick="addNewInputVariable" />
                </div>
            </div>

            <div className="p-4 grid grid-cols-1 gap-4">
                <div>
                    <div>
                        <HScroller scrollIntoViewSelector=".cxb-tab.cxs-active">
                            <Repeater records={bind("$task.conditions")} recordAlias="$con" indexAlias="$index">
                                <Tab
                                    text-bind="$con.tab"
                                    tab-bind="$con.tab"
                                    value-bind="$task.selectedTab"
                                    default-expr="{$index} == 0? true : false"
                                    mod="classic"
                                />
                            </Repeater>
                        </HScroller>
                    </div>
                    <div className="flex flex-1" styles="border: 1px solid lightgray; background: white; padding: 20px;">
                        <Repeater records={bind("$task.conditions")} recordAlias="$con" indexAlias="$indexAlias">
                            <div
                                visible-expr="{$task.selectedTab} == {$con.tab}"
                                className="flex flex-1 flex-col items-center justify-middle"
                            >
                                <div className="flex flex-1" layout={LabelsTopLayout}>
                                    <LookupField label="Source" options-bind="$con.source" value-bind="$con.sourceDecision" />
                                </div>
                                <div className="flex flex-1" layout={LabelsTopLayout}>
                                    <LookupField
                                        label="Param"
                                        options={computable("$con.sourceDecision", "$con.source", (id, sources) => {
                                            return id !== null && sources.length > id ? sources[id].param : [];
                                        })}
                                        value-bind="$con.paramDecision"
                                    />
                                </div>
                            </div>
                        </Repeater>
                    </div>
                </div>
                <div>
                    <ValidationGroup invalid-bind="$task.validExpression">
                        <TextField
                            className="w-full"
                            label="Expression: "
                            value-bind="$task.expression"
                            placeholder="$.Input1 > 27 && $.Input2 < 20ms"
                            required
                            minLength={1}
                            maxLength={100}
                        />
                    </ValidationGroup>
                </div>
                <div>
                    <Button disabled-bind="$task.validExpression" text="Dry run" className="w-full" onClick="openDryWindow" />
                </div>
            </div>
        </div>
    </cx>
);
