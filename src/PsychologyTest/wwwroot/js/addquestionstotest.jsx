var QuestionBox = React.createClass({
    render: function() {
        return(
            <div className="questionBox well">
                <QuestionList />
                <QuestionForm />
            </div>
        );
    }
});

var QuestionList = React.createClass({
    render: function() {
        return(
            <div className="questionList">

            </div>
        );
    }
});

var QuestionForm = React.createClass({
    render: function() {
        return(
            <div className="questionForm">
                <h3>Nueva pregunta</h3>
                <OptionBox />
            </div>
        );
    }
});

var Question = React.createClass({
    render: function() {
        return(
            <div className="question">

            </div>
        );
    }
});

var OptionBox = React.createClass({

    getInitialState: function() {
        return { data: [] };
    },

    handleOptionSubmit: function(option) {
        option.id = this.state.data.length;
        this.state.data.push(option);
        this.setState({ data: this.state.data });
    },

    handleClearOptions: function() {
        this.state.data = [];
        this.setState({ data: this.state.data });
    },

    render: function() {
        return(
            <div className="optionBox">
                <legend>Opciones de respuesta.</legend>
                <OptionList data={this.state.data}/>
                <OptionForm onOptionSubmit={this.handleOptionSubmit} onClearOptions={this.handleClearOptions}/>
            </div>
            );
    }
});

var OptionList = React.createClass({

    render: function () {
        var optionNodes = this.props.data.map(function(option) {
            return(
                <Option value={option.value} text={option.text} key={option.id}>
                </Option>
                );
});
return(
    <div className="optionList">
        {optionNodes}
    </div>
            );
}
});

var OptionForm = React.createClass({

    getInitialState: function() {
        return { text: "" };
    },

    handleTextChange: function(e) {
        this.setState( {text: e.target.value} );
    },

    handleSubmit: function (e) {
        var text = this.state.text.trim();
        if (!text) {
            return;
        }
        this.props.onOptionSubmit({ text: text });
        this.setState({ text: "" });
    },

    clearOptions: function() {
        this.props.onClearOptions();
    },

    render: function() {
        return(
            <div className="optionForm" >
                <div className="form-horizontal">
                    <h4>Nueva opcion de respuesta:</h4>
                    <div className="form-group">
                        <label className="col-lg-2 control-label">Descripcion:</label>
                        <div className="col-lg-10">
                            <input type="text" className="form-control" value={this.state.text} aria-describedby="option-input" onChange={this.handleTextChange}/>
                        </div>
                    </div>
                    
                    <div className="form-group">
                        <div className="col-lg-10 col-lg-offset-2">
                            <button type="button" className="btn btn-warning" onClick={this.clearOptions}>Limpiar opciones</button>
                            <button type="submit" className="btn btn-success" onClick={this.handleSubmit} >Agregar</button>
                        </div>
                    </div>
                    
                </div>                
            </div>
            );
    }
});

var Option = React.createClass({

    render: function() {
        return(
            <div className="option">
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon">
                                <input type="checkbox" disabled />
                        </span>
                        <input type="text" className="form-control" value={this.props.text} />
                    </div>
                </div>
            </div>
            );
    }
});

ReactDOM.render(<QuestionBox />, document.getElementById('content'));