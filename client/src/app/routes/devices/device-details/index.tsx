import { PropertySelection, PureContainer, bind, computable } from 'cx/ui';
import Controller from './Controller';
import { Grid, Heading, Label, List, MsgBox } from 'cx/widgets';

export default () => (
    <cx>
        |<PureContainer controller={Controller}>
            <Heading level="1" text-bind="$page.device.id" />
            <List
                records-bind="$page.device.capabilities"
                selection={PropertySelection}
                style="width:200px"
                emptyText="No results found."
                mod="bordered"
            >
                <div className="flex">
                    <Label text-bind="$record" />
                    <Label text={computable('$page.device', '$record', (device, record) => device)}></Label>
                </div>
            </List>
        </PureContainer>
    </cx>
);

