import { Button, LookupField, Repeater, Tab, ValidationGroup, TextField } from "cx/widgets";
import Controller from "./Controller";
import { LabelsTopLayout, bind } from "cx/ui";
import { openDryRunWindow } from "../dry-run";

export default () => (
    <cx>
        <div class="w-full mt-3 bg-white border-2 border-gray-700 col-span-4 rounded-sm">
            <div className="relative">
                <span className="relative -top-4 left-5 bg-white p-2" text="Condition for execution" />
            </div>

            <div className="p-4 grid grid-cols-1 gap-4" controller={Controller}>
                <div>
                    <div className="flex flex-1 mt-4">
                        <div className="overflow-y-auto h-14">
                            <Repeater records={bind("$task.conditions")} recordAlias="$con" indexAlias="$index">
                                <Tab
                                    text-bind="$con.tab"
                                    tab-bind="$con.tab"
                                    value-bind="$task.selectedTab"
                                    default-expr="{$index} == 0? true : false"
                                    mod="classic"
                                />
                            </Repeater>
                        </div>

                        <Button
                            icon="plus"
                            className="rounded-full h-8 w-8"
                            onClick={(e, { store }) => {
                                let conditions = store.get("$task.conditions");
                                conditions.push({
                                    tab: "Input" + (store.get("$task.conditions").length + 1),
                                    source: [
                                        {
                                            id: 1,
                                            text: "one 3",
                                        },
                                        {
                                            id: 2,
                                            text: "two 3",
                                        },
                                    ],

                                    param: [
                                        {
                                            id: 1,
                                            text: "one 3",
                                        },
                                        {
                                            id: 2,
                                            text: "two 3",
                                        },
                                    ],
                                });
                                store.set("$task.conditions", [...conditions]);
                            }}
                        />
                    </div>
                    <div className="flex flex-1" styles="border: 1px solid lightgray; background: white; padding: 20px">
                        {/* $task.conditions { name: "Task 1", showDescription: false, conditions: [
                                {
                                    id: 1,
                                    source:
                                }] }, 
                        */}
                        <Repeater records={bind("$task.conditions")} recordAlias="$con">
                            <div
                                visible-expr="{$task.selectedTab} == {$con.tab}"
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
                                    <LookupField
                                        label="Param"
                                        options-bind="$con.param"
                                        value-bind="$con.paramDecision"
                                        className="!w-full"
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
                    <Button
                        disabled-bind="$task.validExpression"
                        text="Dry run"
                        className="w-full"
                        onClick={(e, { store }) => {
                            console.log(store.get("$task.validExpression"));
                            // ges Task
                            openDryRunWindow({
                                task: {
                                    inputs: store.get("$task.conditions"),
                                    expression: store.get("$task.expression"),
                                },
                            });
                        }}
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
