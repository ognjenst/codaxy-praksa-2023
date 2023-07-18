import { LabelsLeftLayout, PureContainer, UseParentLayout } from "cx/ui";
import { Heading } from "cx/widgets";
import Controller from "./Controller";
import { BrightnessComponent } from "./BrightnessComponent";
import { StateComponent } from "./StateComponent";
import { ColorComponent } from "./ColorComponent";

export default () => (
    <cx>
        <div controller={Controller} className="p-4">
            <Heading level="1" text-bind="$page.device.id" className="text-4xl" />
            <div className="p-8">
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
        </div>
    </cx>
);
