import Controller from "./Controller";
import { createAccessorModelProxy } from "cx/data";
import { DevicesPageModel } from "./page";
import { FirstVisibleChildLayout, Instance, PureContainer, Repeater, expr } from "cx/ui";
import { Button, Icon, Link, LookupField, Section, TextArea } from "cx/widgets";
import { Status } from "../../types/status";
import { Icons } from "../../types/icons";
import { ButtonMod } from "../../types/buttonMod";

let { $page, $log, $device, $capability } = createAccessorModelProxy<DevicesPageModel>();

export default (
    <cx>
        <div controller={Controller} style={{ display: "flex", flexWrap: "wrap" }}>
            <Section
                style={{ width: "50%" }}
                header={
                    <cx>
                        <div
                            style={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                            }}
                        >
                            <h3>Devices</h3>
                            <Button
                                mod={ButtonMod.Primary}
                                icon={expr($page.status.request, (status) => (status === Status.Loading ? Icons.Loading : Icons.Refresh))}
                                onClick={(_, { controller }: Instance<DevicesPageModel, Controller>) => controller.onGetDevices()}
                                disabled={expr($page.status.request, (status) => status === Status.Loading)}
                            />
                        </div>
                    </cx>
                }
            >
                <FirstVisibleChildLayout>
                    <PureContainer if={expr($page.status.request, (status) => status === Status.Loading)}>
                        <Icon name={Icons.Loading} style={{ paddingRight: "0.5rem" }} />
                        <span>Loading...</span>
                    </PureContainer>
                    <Repeater records={$page.devices} recordAlias={$device}>
                        <div>
                            <span>ID: </span>
                            <span text={$device.id} />
                            <span style={{ paddingLeft: "0.5rem" }}> Capabilities: </span>
                            <Repeater records={$device.capabilities} recordAlias={$capability}>
                                <span text={$capability} />
                                <span ws> </span>
                            </Repeater>
                        </div>
                    </Repeater>
                </FirstVisibleChildLayout>
            </Section>
            
        </div>
    </cx>
);


const gridColumns = [
    {
       header: 'Device name',
       field: 'id',
    },
    {
       header: 'Capabilities',
       field: 'capabilities',
       items: (
          <cx>
             <Link
                href-tpl="~/praksa/{$record.id}"
                url-bind="url"
                text-bind="$record.title"
                className="text-blue-400 hover:text-blue-600"
             />
          </cx>
       ),
    },
    {
       header: 'UserId',
       field: 'userId',
    },
 ];

