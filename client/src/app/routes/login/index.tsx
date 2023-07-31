import Controller from "./Controller";
import { Button, Section, TextField, Link } from "cx/widgets";
export default () => (
    <cx>
        <div
            className="flex flex-col bg-gray-100 h-screen items-center justify-center"
            controller={Controller}
            style={{ backgroundImage: `url('../../resources/background.png')` }}
        >
            <Section className="bg-gray-200 rounded-2xl p-4" style={{ width: "400px", height: "430px" }}>
                <div className="flex flex-col items-center space-y-8">
                    <div className="text-center font-sans text-gray-500 text-2xl">WELCOME</div>

                    <TextField
                        className="h-11"
                        style={{ width: "250px", marginTop: "50px" }}
                        placeholder="Username"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.username"
                        icon="user-circle"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "250px" }}
                        placeholder="Password"
                        inputType="password"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.password"
                        icon="key"
                        required
                    />
                    <Button
                        style={{ width: "250px", marginTop: "50px" }}
                        className="text-xl h-12 bg-gray-100 text-gray-600"
                        onClick="login"
                    >
                        LOGIN
                    </Button>
                    <Link href="~/registration" className="font-sans text-gray-500 text-l bg-transparent underline hover:text-gray-700">
                        Create new account
                    </Link>
                </div>
            </Section>
        </div>
    </cx>
);
