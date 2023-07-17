import Controller from "./Controller";
import { createAccessorModelProxy } from "cx/data";
import { DevicesPageModel } from "./page";
import { FirstVisibleChildLayout, Instance, PureContainer, Repeater, computable, expr } from "cx/ui";
import { Button, Grid, Icon, Text, Link, LookupField, Section, TextArea } from "cx/widgets";
import { Status } from "../../types/status";
import { Icons } from "../../types/icons";
import { ButtonMod } from "../../types/buttonMod";

let { $page, $log, $device, $capability } = createAccessorModelProxy<DevicesPageModel>();

const gridColumns = [
    {
        header: 'Type',
        
    },
    {
        header: 'State',
        field: '$record.state.state',
        items: (
            <cx>
            <Text value={computable('$record.state.state', (state) =>  state ? 'Up' : 'Down')} />
               
            </cx>
         ),
    },
    {
       header: 'Device name',
       field: '$record.id',
       items: (
        <cx>
           <Link
              href-tpl="~/devices/{$record.id}"
              url-bind="url"
              text-bind="$record.id"
              className="text-blue-400 hover:text-blue-600"
           />
        </cx>
     ),
    }
 ];

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
                    

                    <Grid records-bind={$page.devices} columns={gridColumns} scrollable />
                </FirstVisibleChildLayout>
            </Section>
            
        </div>
    </cx>
);




