import { LabelsLeftLayout, PureContainer, UseParentLayout } from "cx/ui";
import { Button, Grid, Heading, LabeledContainer } from "cx/widgets";
import Controller from "./Controller";
import { BrightnessComponent } from "./BrightnessComponent";
import { StateComponent } from "./StateComponent";
import { ColorComponent } from "./ColorComponent";

export default () => (
    <cx>
        <div controller={Controller} className="p-4">
            <Heading level="1" text-bind="$page.device.id" className="text-4xl" />
            <div className="flex-col p-8 space-y-5">
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
                <div className="flex-col space-y-5">
                    <div text="Device history" className="text-2xl" />
                    <Grid records-bind="$page.deviceHistory" headerMode="plain" columns={deviceHistoryColumns} />
                </div>
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
        items: (
            <cx>
                <Button text="Configuration" icon="magnify" onClick="openConfiguration" />
            </cx>
        ),
        sortable: true,
    },
];
