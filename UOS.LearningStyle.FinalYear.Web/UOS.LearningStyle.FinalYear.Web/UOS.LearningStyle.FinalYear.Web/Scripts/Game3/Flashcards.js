(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
        typeof define === 'function' && define.amd ? define(['exports'], factory) :
            (factory((global.pell = {})));
}(this, (function (exports) {
    'use strict';

    var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

    var defaultParagraphSeparatorString = 'defaultParagraphSeparator';
    var formatBlock = 'formatBlock';
    var addEventListener = function addEventListener(parent, type, listener) {
        return parent.addEventListener(type, listener);
    };
    var appendChild = function appendChild(parent, child) {
        return parent.appendChild(child);
    };
    var createElement = function createElement(tag) {
        return document.createElement(tag);
    };
    var queryCommandState = function queryCommandState(command) {
        return document.queryCommandState(command);
    };
    var queryCommandValue = function queryCommandValue(command) {
        return document.queryCommandValue(command);
    };

    var exec = function exec(command) {
        var value = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : null;
        return document.execCommand(command, false, value);
    };

    var defaultActions = {
        bold: {
            icon: '<b>B</b>',
            title: 'Bold',
            state: function state() {
                return queryCommandState('bold');
            },
            result: function result() {
                return exec('bold');
            }
        },
        italic: {
            icon: '<i>I</i>',
            title: 'Italic',
            state: function state() {
                return queryCommandState('italic');
            },
            result: function result() {
                return exec('italic');
            }
        },
        underline: {
            icon: '<u>U</u>',
            title: 'Underline',
            state: function state() {
                return queryCommandState('underline');
            },
            result: function result() {
                return exec('underline');
            }
        },
        strikethrough: {
            icon: '<strike>S</strike>',
            title: 'Strike-through',
            state: function state() {
                return queryCommandState('strikeThrough');
            },
            result: function result() {
                return exec('strikeThrough');
            }
        },
        heading1: {
            icon: '<b>H<sub>1</sub></b>',
            title: 'Heading 1',
            result: function result() {
                return exec(formatBlock, '<h1>');
            }
        },
        heading2: {
            icon: '<b>H<sub>2</sub></b>',
            title: 'Heading 2',
            result: function result() {
                return exec(formatBlock, '<h2>');
            }
        },
        paragraph: {
            icon: '&#182;',
            title: 'Paragraph',
            result: function result() {
                return exec(formatBlock, '<p>');
            }
        },
        quote: {
            icon: '&#8220; &#8221;',
            title: 'Quote',
            result: function result() {
                return exec(formatBlock, '<blockquote>');
            }
        },
        olist: {
            icon: '&#35;',
            title: 'Ordered List',
            result: function result() {
                return exec('insertOrderedList');
            }
        },
        ulist: {
            icon: '&#8226;',
            title: 'Unordered List',
            result: function result() {
                return exec('insertUnorderedList');
            }
        },
        code: {
            icon: '&lt;/&gt;',
            title: 'Code',
            result: function result() {
                return exec(formatBlock, '<pre>');
            }
        },
        line: {
            icon: '&#8213;',
            title: 'Horizontal Line',
            result: function result() {
                return exec('insertHorizontalRule');
            }
        },
        link: {
            icon: '&#128279;',
            title: 'Link',
            result: function result() {
                var url = window.prompt('Enter the link URL');
                if (url) exec('createLink', url);
            }
        },
        image: {
            icon: '&#128247;',
            title: 'Image',
            result: function result() {
                var url = window.prompt('Enter the image URL');
                if (url) exec('insertImage', url);
            }
        }
    };

    var defaultClasses = {
        actionbar: 'pell-actionbar',
        button: 'pell-button',
        content: 'pell-content',
        selected: 'pell-button-selected'
    };

    var init = function init(settings) {
        var actions = settings.actions ? settings.actions.map(function (action) {
            if (typeof action === 'string') return defaultActions[action]; else if (defaultActions[action.name]) return _extends({}, defaultActions[action.name], action);
            return action;
        }) : Object.keys(defaultActions).map(function (action) {
            return defaultActions[action];
        });

        var classes = _extends({}, defaultClasses, settings.classes);

        var defaultParagraphSeparator = settings[defaultParagraphSeparatorString] || 'div';

        var actionbar = createElement('div');
        actionbar.className = classes.actionbar;
        appendChild(settings.element, actionbar);

        var content = settings.element.content = createElement('div');
        content.contentEditable = true;
        content.className = classes.content;
        content.oninput = function (_ref) {
            var firstChild = _ref.target.firstChild;

            if (firstChild && firstChild.nodeType === 3) exec(formatBlock, '<' + defaultParagraphSeparator + '>'); else if (content.innerHTML === '<br>') content.innerHTML = '';
            settings.onChange(content.innerHTML);
        };
        content.onkeydown = function (event) {
            if (event.key === 'Enter' && queryCommandValue(formatBlock) === 'blockquote') {
                setTimeout(function () {
                    return exec(formatBlock, '<' + defaultParagraphSeparator + '>');
                }, 0);
            }
        };
        appendChild(settings.element, content);

        actions.forEach(function (action) {
            var button = createElement('button');
            button.className = classes.button;
            button.innerHTML = action.icon;
            button.title = action.title;
            button.setAttribute('type', 'button');
            button.onclick = function () {
                return action.result() && content.focus();
            };

            if (action.state) {
                var handler = function handler() {
                    return button.classList[action.state() ? 'add' : 'remove'](classes.selected);
                };
                addEventListener(content, 'keyup', handler);
                addEventListener(content, 'mouseup', handler);
                addEventListener(button, 'click', handler);
            }

            appendChild(actionbar, button);
        });

        if (settings.styleWithCSS) exec('styleWithCSS');
        exec(defaultParagraphSeparatorString, defaultParagraphSeparator);

        return settings.element;
    };

    var pell = { exec: exec, init: init };

    exports.exec = exec;
    exports.init = init;
    exports['default'] = pell;

    Object.defineProperty(exports, '__esModule', { value: true });

})));
//event listeners
function eventListeners() {
    const showBtn = document.getElementById("show-btn");
    const questionCard = document.querySelector(".question-card");
    const closeBtn = document.querySelector(".close-btn");
    const form = document.getElementById("question-form");
    const feedback = document.querySelector(".feedback");
    const questionInput = document.getElementById("question-input");
    const answerInput = document.getElementById("answer-input");
    const questionList = document.getElementById("questions-list");
    let data = [];
    let id = 1;

    //new ui instance
    const ui = new UI();

    // show questions form
    showBtn.addEventListener("click", function () {
        ui.showQuestion(questionCard);
    });
    // hide question form
    closeBtn.addEventListener("click", function () {
        ui.hideQuestion(questionCard);
    });
    // add question
    form.addEventListener("submit", function (event) {
        event.preventDefault();

        const questionValue = questionInput.value;
        const answerValue = answerInput.value;

        if (questionValue === "" || answerValue === "") {
            feedback.classList.add('showItem', 'alert-danger');
            feedback.textContent = "Cannot add empty values!";

            setTimeout(function () {
                feedback.classList.remove('alert-danger', 'showItem');
            }, 4000);
        }
        else {
            const question = new Question(id, questionValue, answerValue);
            data.push(question)
            id++;
            ui.clearFields(questionInput, answerInput);
            ui.addQuestion(questionList, question);
        }
    });
    // work with a question
    questionList.addEventListener('click', function (event) {
        event.preventDefault();
        if (event.target.classList.contains('delete-flashcard')) {
            console.log('delete flashcard!');
            let id = event.target.dataset.id;
            questionList.removeChild(event.target.parentElement.parentElement.parentElement);
            let tempData = data.filter(function (item) {
                return item.id !== parseInt(id);
            });
            data = tempData;
        }
        else if (event.target.classList.contains('show-answer')) {
            console.log('show flashcard!');
            event.target.nextElementSibling.classList.toggle('showItem')
        }
        else if (event.target.classList.contains('edit-flashcard')) {
            console.log('edit flashcard!');

            //delete question
            let id = event.target.dataset.id;

            // delete question from dom
            questionList.removeChild(event.target.parentElement.parentElement.parentElement)
            // show the question card
            ui.showQuestion(questionCard);
            //specific question
            const tempQuestion = data.filter(function (item) {
                return item.id === parseInt(id);
            })
            //rest of the data
            let tempData = data.filter(function (item) {
                return item.id !== parseInt(id)
            })

            data = tempData;
            questionInput.value = tempQuestion[0].title;
            answerInput.value = tempQuestion[0].answer;
        }

    })
}
// ui constructor
function UI() { }
//show question card
UI.prototype.showQuestion = function (element) {
    element.classList.add('showItem');
};
// hide question card
UI.prototype.hideQuestion = function (element) {
    element.classList.remove('showItem');
};
//add question
UI.prototype.addQuestion = function (element, question) {
    const div = document.createElement("div");
    div.classList.add('col-md-4');
    div.innerHTML = `
    <div class="card card-body flashcard my-3">
     <h4 class="text-capitalize">${question.title}</h4>
     <a href="#" class="text-capitalize my-3 show-answer">Show contents</a>
     <h5 class="answer mb-3">${question.answer}</h5>
     <div class="flashcard-btn d-flex justify-content-between">
        <a href="#" id="edit-flashcard" class=" btn my-1 edit-flashcard text-uppercase" data-id="${question.id}">edit</a>
        <a href="#" id="delete-flashcard" class=" btn my-1 delete-flashcard text-uppercase
        data-id="${question.id}"
      ">delete</a>
     </div>
    </div>
    `;
    element.appendChild(div);
};

//clear fields
UI.prototype.clearFields = function (question, answer) {
    question.value = "";
    answer.value = "";
};

// question constructor
function Question(id, title, answer) {
    this.id = id;
    this.title = title;
    this.answer = answer;
}

//dom event listener
document.addEventListener('DOMContentLoaded', function () {
    eventListeners();

});