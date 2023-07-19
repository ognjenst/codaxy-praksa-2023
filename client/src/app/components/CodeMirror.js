import { CodeMirror } from '../../app/components/CodeMirror';

import codemirror from 'codemirror';
import 'codemirror/addon/edit/closetag';
import 'codemirror/addon/edit/matchtags';
import 'codemirror/addon/selection/active-line';
import 'codemirror/mode/clike/clike';
import 'codemirror/mode/javascript/javascript';
import 'codemirror/mode/python/python';
import { CSS, getContent, VDOM, Widget } from 'cx/ui';
import { isString } from 'cx/util';
import { TextField } from 'cx/widgets';

export class CodeMirror extends Widget {
    declareData() {
        return super.declareData(...arguments, {
            code: undefined,
            className: { structured: true },
            class: { structured: true },
            style: { structured: true },
            onSave: undefined,
            lineSeparator: '\n',
        });
    }

    render(context, instance, key) {
        return (
            <Component
                key={key}
                instance={instance}
                data={instance.data}
                help={this.helpPlacement && getContent(this.renderHelp(context, instance, 'help'))}
            />
        );
    }
}

CodeMirror.prototype.baseClass = 'codemirror';

class Component extends VDOM.Component {
    constructor(props) {
        super(props);
        this.state = {
            focus: false,
        };
    }

    render() {
        let { help } = this.props;
        var { data, widget } = this.props.instance;
        return (
            <div className={data.classNames} style={data.style}>
                <textarea className={CSS.element(widget.baseClass, 'input')} defaultValue={data.code} ref="input" />
                {help}
            </div>
        );
    }

    shouldComponentUpdate() {
        return false;
    }

    componentDidMount() {
        var { widget } = this.props.instance;
        this.cm = codemirror.fromTextArea(this.refs.input, {
            lineNumbers: true,
            autoRefresh: true,
            fixedGutter: false,
            mode: widget.mode,
            tabSize: 4,
            matchTags: { bothTags: true },
            autoCloseTags: true,
            styleActiveLine: true,
            lineSeparator: widget.lineSeparator,
            readOnly: widget.readOnly,
            extraKeys: {
                // 'Ctrl-S': () => this.doSave(),
                // 'Ctrl-I': () => this.resolveImport()
            },
        });
        this.cm.on('blur', () => this.onBlur());
    }

    componentWillReceiveProps(props) {
        if (props.data.code != this.cm.getValue()) this.cm.setValue(props.data.code || '');
    }

    save() {
        var { widget, store } = this.props.instance;
        if (widget.nameMap.code) {
            var value = this.cm.getValue();
            if (typeof value == 'string') store.set(widget.nameMap.code, value);
        }
    }

    doSave() {
        this.save();

        let { data, controller } = this.props.instance;
        if (isString(data.onSave)) controller[data.onSave]();
        else onSave();
    }

    onBlur() {
        this.save();
    }
}

CodeMirror.prototype.mode = 'application/json';