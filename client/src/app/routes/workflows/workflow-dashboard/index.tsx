import { Button, FlexRow, Icon, Switch } from "cx/widgets";

export default () => (
    <cx>
        <div className="relative">
            <span className="relative -top-4 left-5 bg-white p-2">Morning Routine</span>
            <Button className="absolute top-2 right-2 p-0" mod="hollow"><Icon className="h-10 w-10" name="calendar"/></Button>
        </div> 

        <div className="p-10">
            <div className="flex flex-row">
                <div className="flex flex-col">
                    <div className="flex-1"><FlexRow align="center" className="h-full m-0">Enabled</FlexRow></div>
                    <div className="flex-1"><FlexRow align="center" className="h-full m-0">Status</FlexRow></div>
                </div>
                <div className="flex flex-col">
                    <div className="flex-1"><Switch rangeStyle={highlightBackgroundColor} off-bind="$page.check"/></div>
                    <div className="flex-1">
                        <FlexRow align="center" className="h-full m-0">
                            <Switch off-bind="$page.check"/>
                            <Switch off-bind="$page.check"/>
                            <Switch off-bind="$page.check"/>
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6 text-red-800">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                                <path stroke-linecap="round" stroke-linejoin="round" d="M9 9.563C9 9.252 9.252 9 9.563 9h4.874c.311 0 .563.252.563.563v4.874c0 .311-.252.563-.563.563H9.564A.562.562 0 019 14.437V9.564z" />
                            </svg>

                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6 text-red-800">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M14.25 9v6m-4.5 0V9M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>


                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6 text-red-800">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                                <path stroke-linecap="round" stroke-linejoin="round" d="M15.91 11.672a.375.375 0 010 .656l-5.603 3.113a.375.375 0 01-.557-.328V8.887c0-.286.307-.466.557-.327l5.603 3.112z" />
                            </svg>


                        </FlexRow>
                    </div>
                </div>
            </div>
        </div>
    </cx>
);

const highlightBackgroundColor = "background:green";