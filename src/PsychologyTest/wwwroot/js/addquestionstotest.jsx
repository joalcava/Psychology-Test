var QuestionBox = React.createClass({
    
    getInitialState: function() {
        return { data: [{id: 1, position: 1, description: "Pregunta de prueba", type: "closedshort", options: [{id:1, text: "Opcion de prueba"}, {id:2, text: "Otra opcion de prueba"}]}] };
    },

    loadQuestionsFromServer: function() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },

    componentDidMount: function() {
        this.loadQuestionsFromServer();
        //window.setInterval(this.loadQuestionsFromServer, this.props.pollInterval);
    },

    handleQuestionSubmit: function(option) {
        
    },

    render: function () {
        return (
            <div className="questionBox">
                <QuestionList data={this.state.data}/>
                <QuestionForm />
            </div>
        );
    }
});

var QuestionList = React.createClass({
    render: function () {
        var questionNodes = this.props.data.map(function(question) {
            return (
                <Question position={question.position} description={question.description} key={question.id}>
                    {String(question.options)}
                </Question>
                );
        });
        return (
            <div className="questionList">
                {questionNodes}
            </div>
        );
    }
});

var QuestionForm = React.createClass({

    getInitialState: function() {
        return { type: "", position: "", description: "", options: [] };
    },

    handlePositionChange: function(e) {
        this.setState({ position: e.target.value });
    },

    handleDescriptionChange: function(e) {
        this.setState({ description: e.target.value });
    },

    handleOptionSubmit: function (data) {
        this.state.options = data;
        this.setState({ options: data });
        console.log(this.state.position);
        console.log(this.state.description);
        console.log(this.state.options);
        console.log(this.state.type);
    },

    handleQuestionTypeChange: function () {
        var select = document.getElementById("questionType");
        var op = select.selectedIndex;
        if (op === 0) {
            ReactDOM.render(<div></div>, document.getElementById("questionOptionsContent"));
        } else if (op === 1) {
            this.setState({ type: "openshort"});
            ReactDOM.render(<div></div>, document.getElementById("questionOptionsContent"));
        } else if (op === 2) {
            this.setState({ type: "opendlong" });
            ReactDOM.render(<div></div>, document.getElementById("questionOptionsContent"));
        } else if (op === 3) {
            this.setState({ type: "closedunique" });
            ReactDOM.render(<OptionBox onOptionSubmit={this.handleOptionSubmit}></OptionBox>, document.getElementById("questionOptionsContent"));
        } else if (op === 4) {
            this.setState({ type: "closedmultiple" });
            ReactDOM.render(<OptionBox onOptionSubmit={this.handleOptionSubmit}></OptionBox>, document.getElementById("questionOptionsContent"));
        } else if (op === 5) {
            this.setState({ type: "closedmulitplevaluated" });
            ReactDOM.render(<OptionBox onOptionSubmit={this.handleOptionSubmit}></OptionBox>, document.getElementById("questionOptionsContent"));
        }
    },

    render: function () {
        return (
            <div className="questionForm ">
                <form className="form-horizontal well">
                    <fieldset>
                        <h3>Nueva pregunta</h3>
                        <div className="form-group col-sm-2">
                            <label>Posicion</label>
                            <input className="form-control" type="number" value={this.state.position} onChange={this.handlePositionChange}/>
                        </div>
                        <div className="form-group col-sm-10">
                            <label>Descripcion</label>
                            <input className="form-control" value={this.state.description} onChange={this.handleDescriptionChange}/>
                        </div>
                        <div className="form-group col-sm-12">
                            <label>Tipo de pregunta</label>
                            <select className="form-control" id="questionType" onChange={this.handleQuestionTypeChange}>
                                <option value="0"></option>
                                <option value="1">De respuesta abierta corta</option>
                                <option value="2">De respuesta abierta larga</option>
                                <option value="3">De opcion multiple, unica respuesta</option>
                                <option value="4">De opcion multiple, multiples respuestas</option>
                                <option value="5">De opcion multiple, multiples respuestas con valor de verdad</option>
                            </select>
                        </div>
                        <div id="questionOptionsContent"></div>
                        
                        <div className="form-group col-sm-12">
                            <div>
                                <button className="btn btn-primary btn-lg" type="button">Volver</button>
                                <button className="btn btn-success btn-lg" type="submit">Enviar</button>
                            </div>
                        </div>
                    </fieldset>
                </form>
            </div>
        );
    }
});

var Question = React.createClass({
    render: function () {
        return (
            <div className="question">
                <h4>{this.props.position} - {this.props.description}</h4>
                {this.props.children}
            </div>
        );
    }
});

var OptionBox = React.createClass({

    getInitialState: function () {
        return { data: [] };
    },

    handleOptionSubmit: function (option) {
        option.id = this.state.data.length;
        this.state.data.push(option);
        this.setState({ data: this.state.data });
        this.props.onOptionSubmit(this.state.data);
    },

    handleClearOptions: function () {
        this.state.data = [];
        this.setState({ data: this.state.data });
    },

    render: function () {
        return (
            <div className="optionBox">
                <h3>Opciones de respuesta.</h3>
                <OptionList data={this.state.data} />
                <OptionForm onOptionSubmit={this.handleOptionSubmit} onClearOptions={this.handleClearOptions} />
            </div>
            );
    }
});

var OptionList = React.createClass({

    render: function () {
        var optionNodes = this.props.data.map(function (option) {
            return (
                <Option value={option.value} text={option.text} key={option.id}></Option>
                );
        });
        return (
    <div className="optionList">
        {optionNodes}
    </div>
            );
    }
});

var OptionForm = React.createClass({

    getInitialState: function () {
        return { text: "" };
    },

    handleTextChange: function (e) {
        this.setState({ text: e.target.value });
    },

    handleSubmit: function (e) {
        var text = this.state.text.trim();
        if (!text) {
            return;
        }
        this.props.onOptionSubmit({ text: text });
        this.setState({ text: "" });
    },

    clearOptions: function () {
        this.props.onClearOptions();
    },

    render: function () {
        return (
            <div className="optionForm">
                <hr />
                <div className="form-horizontal col-lg-12">
                    <h4>Nueva opcion de respuesta:</h4>
                    <div className="form-group">
                        <label className="col-lg-2 control-label">Descripcion:</label>
                        <div className="col-lg-10">
                            <input type="text" className="form-control" value={this.state.text} aria-describedby="option-input" onChange={this.handleTextChange} />
                        </div>
                    </div>

                    <div className="form-group">
                        <div className="col-lg-10 col-lg-offset-2">
                            <button type="button" className="btn btn-default" onClick={this.clearOptions}>Limpiar opciones</button>
                            <button type="button" className="btn btn-primary" onClick={this.handleSubmit}>Agregar</button>
                        </div>
                    </div>
                    <hr/>
                </div>
            </div>
            );
    }
});

var Option = React.createClass({

    render: function () {
        return (
            <div className="option">
                <div className="form-group col-lg-12">
                    <div className="input-group ">
                        <span className="input-group-addon">
                                <input type="checkbox" disabled />
                        </span>
                        <input type="text" className="form-control" readOnly value={this.props.text} />
                    </div>
                </div>
            </div>
            );
    }
});
var testid = document.getElementById("testid").value;
var getquestionsurl = "/Admin/GetQuestions?testId=" + testid;
ReactDOM.render(<QuestionBox url={getquestionsurl} pollInterval={10000} />, document.getElementById('content'));