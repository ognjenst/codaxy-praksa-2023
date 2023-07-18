import { Button, FlexRow, Grid, Icon, Switch } from "cx/widgets";

export default () => (
    <cx>
        <div className="relative">
            <span className="relative -top-4 left-5 bg-white p-2">Morning Routine</span>
            <Button className="absolute top-2 right-2 p-0" mod="hollow">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-8 h-8">
                <path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" />
            </svg>

            </Button>
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

            <div className="flex flex-col flex-1 mt-10">
                <div class="mt-3 md:mt-0 lg:mt-0 bg-white border-2 border-gray-700 col-span-4 rounded-sm">
                    <div className="relative">
                        <span className="relative -top-4 left-5 bg-white p-2">Tasks</span>

                        <Grid cached style={{width: "100%"}}>

                        </Grid>
                    </div> 
                </div>
            </div>
        </div>
    </cx>
);

const highlightEnabledBackgroundColor = "background:#00FF00";

const defaultStatusColor = "text-white bg-black";
const highlightStatusColor = "text-white bg-green-600";