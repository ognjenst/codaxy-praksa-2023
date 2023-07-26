import { LabelsLeftLayout, PureContainer, Text, UseParentLayout } from "cx/ui";
import { CategoryAxis, Chart, ColumnGraph, Gridlines, Legend, LineGraph, Marker, NumericAxis, Range, TimeAxis } from "cx/charts";
import { ClipRect, Rectangle, Svg } from "cx/svg";
import { Button, Grid, Heading, LabeledContainer, TextField, Window } from "cx/widgets";
import Controller from "./Controller";
import { BrightnessComponent } from "./BrightnessComponent";
import { StateComponent } from "./StateComponent";
import { ColorComponent } from "./ColorComponent";
import { EnergyComponent } from "./EnergyComponent";
import { TemperatureComponent } from "./TemperatureComponent";
import { HumidityComponent } from "./HumidityComponent";
import { ContactComponent } from "./ContactComponent";
import { PowerGraph } from "./PowerGraph";
import { TemperatureGraph } from "./TemperatureGraph";
import { HumidityGraph } from "./HumidityGraph";

export default () => (
    <cx>
        <div controller={Controller} className="p-4 flex flex-col gap-4 overflow-hidden">
            <Heading level="1" text-bind="$page.device.name" className="text-2xl text-slate-900" />
            <div text-bind="$page.device.id" className=" text-slate-600" />
            <hr />

            <div className="flex-col px-8 py-4 space-y-5">
                <div className="flex">
                    <div text="Description" className="flex-1 text-slate-600" />
                    <div text-bind="$page.device.description" className="flex-1 text-slate-600" />
                </div>
                <div className="flex space-x-50">
                    <div text="Manufacturer" className="flex-1 text-slate-600" />
                    <div text-bind="$page.device.manufacturer" className="flex-1 text-slate-600" />
                </div>
                <div className="flex space-x-50">
                    <div text="Type" className="flex-1 text-slate-600" />
                    <div text-bind="$page.device.type" className="flex-1 text-slate-600" />
                </div>
            </div>
            <hr className="mx-8" />
            <div className="px-8 flex">
                <div className="flex items-center flex-1">
                    <LabelsLeftLayout className="device-control">
                        <PureContainer layout={UseParentLayout} visible-bind="$page.device.state">
                            <StateComponent />
                        </PureContainer>
                        <PureContainer layout={UseParentLayout} visible-bind="$page.device.light">
                            <BrightnessComponent />
                        </PureContainer>
                        <PureContainer layout={UseParentLayout} visible-bind="$page.device.colorXy">
                            <ColorComponent />
                        </PureContainer>
                        <PureContainer layout={UseParentLayout} visible-expr="{$page.device.energy.power} != null">
                            <EnergyComponent />
                        </PureContainer>
                        <PureContainer layout={UseParentLayout} visible-expr="{$page.device.temperature}">
                            <TemperatureComponent />
                        </PureContainer>
                        <PureContainer layout={UseParentLayout} visible-expr="{$page.device.humidity}">
                            <HumidityComponent />
                        </PureContainer>
                        <PureContainer layout={UseParentLayout} visible-bind="{$page.device.contact}">
                            <ContactComponent />
                        </PureContainer>
                    </LabelsLeftLayout>
                </div>
                <div className="flex items-center flex-1">
                    <PureContainer layout={UseParentLayout} visible-expr="{$page.device.energy.power} != null">
                        <PowerGraph />
                    </PureContainer>
                </div>
                <div className="flex flex-col">
                    <div className="flex">
                        <div className="flex items-center flex-1">
                            <PureContainer layout={UseParentLayout} visible-expr="{$page.device.temperature}">
                                <TemperatureGraph />
                            </PureContainer>
                        </div>
                        <div className="flex items-center flex-1">
                            <PureContainer layout={UseParentLayout} visible-expr="{$page.device.humidity}">
                                <HumidityGraph />
                            </PureContainer>
                        </div>
                    </div>
                    <Legend visible-bind="$page.device.temperature" />
                </div>
            </div>
            <hr className="mx-8" />
            <div text="Device history" className="px-8 py-4 text-slate-600" />
            <div className="px-8 flex-1 overflow-y-auto">
                <Grid
                    className="text-slate-600 h-full"
                    records-bind="$page.deviceHistory"
                    headerMode="plain"
                    sortField="time"
                    columns={deviceHistoryColumns}
                    scrollable
                    buffered
                />
            </div>
        </div>
    </cx>
);

const deviceHistoryColumns = [
    {
        header: "Timestamp",
        field: "time",
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
