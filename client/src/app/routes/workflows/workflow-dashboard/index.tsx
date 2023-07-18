import { Button, FlexRow, Icon, Switch } from "cx/widgets";

export default () => (
    <cx>
        <div className="relative">
            <span className="relative -top-4 left-5 bg-white p-2" text-bind="$page.currentWorkflow.name" />
            <Button className="absolute top-2 right-2 p-0" mod="hollow"><Icon className="h-10 w-10" name="calendar"/></Button>
        </div> 

        <div className="p-10">
            <div className="flex flex-row">
                <div className="flex flex-col">
                    <div className="flex-1"><FlexRow align="center" className="h-full m-0">Enabled</FlexRow></div>
                    <div className="flex-1"><FlexRow align="center" className="h-full m-0">Status</FlexRow></div>
                </div>
                <div className="flex flex-col">
                    <div className="flex-1"><Switch rangeStyle={highlightEnabledBackgroundColor} off-bind="$page.check"/></div>
                    <div className="flex-1">
                        <FlexRow align="center" className="h-full">
                            <Button class="rounded-full p-1 bg-green-600 m-1">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M5.25 7.5A2.25 2.25 0 017.5 5.25h9a2.25 2.25 0 012.25 2.25v9a2.25 2.25 0 01-2.25 2.25h-9a2.25 2.25 0 01-2.25-2.25v-9z" />
                                </svg>

                            </Button>

                            <Button class="rounded-full p-1 bg-green-600 m-1">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 5.25v13.5m-7.5-13.5v13.5" />
                                </svg>

                            </Button>

                            <Button class="rounded-full p-1 bg-green-600 m-1">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M5.25 5.653c0-.856.917-1.398 1.667-.986l11.54 6.348a1.125 1.125 0 010 1.971l-11.54 6.347a1.125 1.125 0 01-1.667-.985V5.653z" />
                                </svg>

                            </Button>


                        </FlexRow>
                    </div>
                </div>
            </div>
        </div>
    </cx>
);

const highlightEnabledBackgroundColor = "background:green";

const defaultStatusColor = "text-white bg-black";
const highlightStatusColor = "text-white bg-green-600";