import { Icon } from "cx/widgets";
import { VDOM } from "cx/ui";
import {
    AdjustmentsHorizontalIcon,
    ArrowDownIcon,
    ArrowLeftIcon,
    ArrowUpIcon,
    CalendarIcon,
    BanknotesIcon,
    ChartBarIcon,
    ChevronDownIcon,
    ChevronUpIcon,
    CogIcon,
    CreditCardIcon,
    CurrencyDollarIcon,
    CurrencyEuroIcon,
    DocumentChartBarIcon,
    DocumentTextIcon,
    ShieldExclamationIcon,
    InformationCircleIcon,
    PencilIcon,
    PlusIcon,
    PlusCircleIcon,
    PresentationChartBarIcon,
    PrinterIcon,
    PuzzlePieceIcon,
    ArrowPathIcon,
    MagnifyingGlassIcon,
    RectangleGroupIcon,
    UserGroupIcon,
    UserIcon,
    UsersIcon,
    Bars4Icon,
    XMarkIcon,
    LightBulbIcon,
    ClockIcon,
    ArrowDownCircleIcon,
    PresentationChartLineIcon,
    PauseCircleIcon,
    SignalIcon,
    PlayCircleIcon,
    PlayIcon,
    StopCircleIcon,
    TrashIcon,
    MinusIcon,
    UserCircleIcon,
    EnvelopeIcon,
    KeyIcon,
    BoltIcon,
    ShieldCheckIcon,
    WrenchScrewdriverIcon,
} from "@heroicons/react/24/outline";

//register all icons that are used within the application

Icon.register("chart-bar", (props) => <ChartBarIcon {...props} />);

Icon.register("adjustments", (props) => <AdjustmentsHorizontalIcon {...props} />);

Icon.register("document-report", (props) => <DocumentChartBarIcon {...props} />);

Icon.register("view-list", (props) => <Bars4Icon {...props} />);

Icon.register("presentation-chart-bar", (props) => <PresentationChartBarIcon {...props} />);

Icon.register("search", (props) => <MagnifyingGlassIcon {...props} />);

Icon.register("calendar", (props) => <CalendarIcon {...props} />);

Icon.register("template", (props) => <RectangleGroupIcon {...props} />);

Icon.register("puzzle", (props) => <PuzzlePieceIcon {...props} />);

Icon.register("cash", (props) => <BanknotesIcon {...props} />);

Icon.register("arrow-up", (props) => <ArrowUpIcon {...props} />);
Icon.register("arrow-down", (props) => <ArrowDownIcon {...props} />);

Icon.register("exclamation", (props) => <ShieldExclamationIcon {...props} />);

Icon.register("credit-card", (props) => <CreditCardIcon {...props} />);

Icon.register("document-text", (props) => <DocumentTextIcon {...props} />);

Icon.register("cog", (props) => <CogIcon {...props} />);

Icon.register("adjustments", (props) => <AdjustmentsIcon {...props} />);

Icon.register("users", (props) => <UsersIcon {...props} />);

Icon.register("user", (props) => <UserIcon {...props} />);

Icon.register("user-group", (props) => <UserGroupIcon {...props} />);

Icon.register("trash", (props) => <TrashIcon {...props} />);

Icon.register("currency-dollar", (props) => <CurrencyDollarIcon {...props} />);

Icon.register("currency-euro", (props) => <CurrencyEuroIcon {...props} />);

Icon.register("chevron-down", (props) => <ChevronDownIcon {...props} />);
Icon.register("drop-down", (props) => <ChevronDownIcon {...props} />);
Icon.register("drop-up", (props) => <ChevronUpIcon {...props} />);

Icon.register("information-circle", (props) => <InformationCircleIcon {...props} />);

Icon.register("refresh", (props) => <ArrowPathIcon {...props} />);

Icon.register("x", (props) => <XMarkIcon {...props} />);
Icon.register("close", (props) => <XMarkIcon {...props} />);

Icon.register("plus", (props) => <PlusIcon {...props} />);
Icon.register("plus-circle", (props) => <PlusCircleIcon {...props} />);

Icon.register("arrow-left", (props) => <ArrowLeftIcon {...props} />);

Icon.register("printer", (props) => <PrinterIcon {...props} />);

Icon.register("pencil", (props) => <PencilIcon {...props} />);

Icon.register("light-bulb", (props) => <LightBulbIcon {...props} />);

Icon.register("clock", (props) => <ClockIcon {...props} />);

Icon.register("arrow-down-circle", (props) => <ArrowDownCircleIcon {...props} />);

Icon.register("presentation-chart-line", (props) => <PresentationChartLineIcon {...props} />);

Icon.register("pause-circle", (props) => <PauseCircleIcon {...props} />);

Icon.register("signal", (props) => <SignalIcon {...props} />);

Icon.register("play-circle", (props) => <PlayCircleIcon {...props} />);

Icon.register("play", (props) => <PlayIcon {...props} />);

Icon.register("stop-circle", (props) => <StopCircleIcon {...props} />);

Icon.register("user-circle", (props) => <UserCircleIcon {...props} />);

Icon.register("envelope", (props) => <EnvelopeIcon {...props} />);

Icon.register("key", (props) => <KeyIcon {...props} />);
Icon.register("minus", (props) => <MinusIcon {...props} />);

Icon.register("bolt", (props) => <BoltIcon {...props} />);

Icon.register("shield-check", (props) => <ShieldCheckIcon {...props} />);

Icon.register("wrench-screwdriver", (props) => <WrenchScrewdriverIcon {...props} />);
