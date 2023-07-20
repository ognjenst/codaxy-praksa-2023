import { LabelsLeftLayout, PureContainer, Text, UseParentLayout } from "cx/ui";
import { Button, Grid, Heading, LabeledContainer, TextField, Window } from "cx/widgets";
import Controller from "./Controller";
import { BrightnessComponent } from "./BrightnessComponent";
import { StateComponent } from "./StateComponent";
import { ColorComponent } from "./ColorComponent";

export default () => (
    <cx>
        <div controller={Controller} className="p-4 flex flex-col overflow-hidden gap-4">
            <Heading level="1" text-bind="$page.device.name" className="text-2xl text-slate-900" />
            <div text-bind="$page.device.id" className="text-l text-slate-600" />
            <hr />

            <div className="flex-col p-8 space-y-5">
                <div className="flex">
                    <div text="Description" className="flex-1 text-l text-slate-600" />
                    <div text-bind="$page.device.description" className="flex-1 text-l text-slate-600" />
                </div>
                <div className="flex space-x-50">
                    <div text="Manufacturer" className="flex-1 text-l text-slate-600" />
                    <div text-bind="$page.device.manufacturer" className="flex-1 text-l text-slate-600" />
                </div>
                <div className="flex space-x-50">
                    <div text="Type" className="flex-1 text-l text-slate-600" />
                    <div text-bind="$page.device.type" className="flex-1 text-l text-slate-600" />
                </div>
            </div>
            <hr />
            <div>
                <LabelsLeftLayout>
                    <PureContainer layout={UseParentLayout} visible-expr="{$page.device.state} !== null">
                        <StateComponent />
                    </PureContainer>
                    <PureContainer layout={UseParentLayout} visible-bind="$page.device.light">
                        <BrightnessComponent />
                    </PureContainer>
                    <PureContainer layout={UseParentLayout} visible-bind="$page.device.colorXy">
                        <ColorComponent />
                    </PureContainer>
                </LabelsLeftLayout>
            </div>
            <hr />

            <div text="Device history" className="text-lg text-slate-600" />
            <div className="flex-1 overflow-y-auto">
                <Grid
                    className="text-slate-600 h-full"
                    records-bind="$page.deviceHistory"
                    headerMode="plain"
                    sortField="timestamp"
                    columns={deviceHistoryColumns}
                    scrollable
                />
            </div>
        </div>
    </cx>
);

const deviceHistoryColumns = [
    {
        header: "Timestamp",
        field: "timestamp",
        sortable: true,
    },
    {
        header: "Configuration",
        field: "configuration",
        defaultWidth: 200,
        items: (
            <cx>
                <Button text="More information" icon="document-report" onClick="showHistoryWindow" />
            </cx>
        ),
        sortable: true,
    },
];
