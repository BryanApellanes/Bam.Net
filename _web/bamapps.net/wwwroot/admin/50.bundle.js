(self["webpackJsonp"] = self["webpackJsonp"] || []).push([[50],{

/***/ "./node_modules/monaco-editor/esm/vs/basic-languages/vb/vb.js":
/*!********************************************************************!*\
  !*** ./node_modules/monaco-editor/esm/vs/basic-languages/vb/vb.js ***!
  \********************************************************************/
/*! exports provided: conf, language */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"conf\", function() { return conf; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"language\", function() { return language; });\n/*---------------------------------------------------------------------------------------------\r\n *  Copyright (c) Microsoft Corporation. All rights reserved.\r\n *  Licensed under the MIT License. See License.txt in the project root for license information.\r\n *--------------------------------------------------------------------------------------------*/\r\n\r\nvar conf = {\r\n    comments: {\r\n        lineComment: '\\'',\r\n        blockComment: ['/*', '*/'],\r\n    },\r\n    brackets: [\r\n        ['{', '}'], ['[', ']'], ['(', ')'], ['<', '>'],\r\n        ['addhandler', 'end addhandler'],\r\n        ['class', 'end class'],\r\n        ['enum', 'end enum'],\r\n        ['event', 'end event'],\r\n        ['function', 'end function'],\r\n        ['get', 'end get'],\r\n        ['if', 'end if'],\r\n        ['interface', 'end interface'],\r\n        ['module', 'end module'],\r\n        ['namespace', 'end namespace'],\r\n        ['operator', 'end operator'],\r\n        ['property', 'end property'],\r\n        ['raiseevent', 'end raiseevent'],\r\n        ['removehandler', 'end removehandler'],\r\n        ['select', 'end select'],\r\n        ['set', 'end set'],\r\n        ['structure', 'end structure'],\r\n        ['sub', 'end sub'],\r\n        ['synclock', 'end synclock'],\r\n        ['try', 'end try'],\r\n        ['while', 'end while'],\r\n        ['with', 'end with'],\r\n        ['using', 'end using'],\r\n        ['do', 'loop'],\r\n        ['for', 'next']\r\n    ],\r\n    autoClosingPairs: [\r\n        { open: '{', close: '}', notIn: ['string', 'comment'] },\r\n        { open: '[', close: ']', notIn: ['string', 'comment'] },\r\n        { open: '(', close: ')', notIn: ['string', 'comment'] },\r\n        { open: '\"', close: '\"', notIn: ['string', 'comment'] },\r\n        { open: '<', close: '>', notIn: ['string', 'comment'] },\r\n    ],\r\n    folding: {\r\n        markers: {\r\n            start: new RegExp(\"^\\\\s*#Region\\\\b\"),\r\n            end: new RegExp(\"^\\\\s*#End Region\\\\b\")\r\n        }\r\n    }\r\n};\r\nvar language = {\r\n    defaultToken: '',\r\n    tokenPostfix: '.vb',\r\n    ignoreCase: true,\r\n    brackets: [\r\n        { token: 'delimiter.bracket', open: '{', close: '}' },\r\n        { token: 'delimiter.array', open: '[', close: ']' },\r\n        { token: 'delimiter.parenthesis', open: '(', close: ')' },\r\n        { token: 'delimiter.angle', open: '<', close: '>' },\r\n        // Special bracket statement pairs\r\n        // according to https://msdn.microsoft.com/en-us/library/tsw2a11z.aspx\r\n        { token: 'keyword.tag-addhandler', open: 'addhandler', close: 'end addhandler' },\r\n        { token: 'keyword.tag-class', open: 'class', close: 'end class' },\r\n        { token: 'keyword.tag-enum', open: 'enum', close: 'end enum' },\r\n        { token: 'keyword.tag-event', open: 'event', close: 'end event' },\r\n        { token: 'keyword.tag-function', open: 'function', close: 'end function' },\r\n        { token: 'keyword.tag-get', open: 'get', close: 'end get' },\r\n        { token: 'keyword.tag-if', open: 'if', close: 'end if' },\r\n        { token: 'keyword.tag-interface', open: 'interface', close: 'end interface' },\r\n        { token: 'keyword.tag-module', open: 'module', close: 'end module' },\r\n        { token: 'keyword.tag-namespace', open: 'namespace', close: 'end namespace' },\r\n        { token: 'keyword.tag-operator', open: 'operator', close: 'end operator' },\r\n        { token: 'keyword.tag-property', open: 'property', close: 'end property' },\r\n        { token: 'keyword.tag-raiseevent', open: 'raiseevent', close: 'end raiseevent' },\r\n        { token: 'keyword.tag-removehandler', open: 'removehandler', close: 'end removehandler' },\r\n        { token: 'keyword.tag-select', open: 'select', close: 'end select' },\r\n        { token: 'keyword.tag-set', open: 'set', close: 'end set' },\r\n        { token: 'keyword.tag-structure', open: 'structure', close: 'end structure' },\r\n        { token: 'keyword.tag-sub', open: 'sub', close: 'end sub' },\r\n        { token: 'keyword.tag-synclock', open: 'synclock', close: 'end synclock' },\r\n        { token: 'keyword.tag-try', open: 'try', close: 'end try' },\r\n        { token: 'keyword.tag-while', open: 'while', close: 'end while' },\r\n        { token: 'keyword.tag-with', open: 'with', close: 'end with' },\r\n        // Other pairs\r\n        { token: 'keyword.tag-using', open: 'using', close: 'end using' },\r\n        { token: 'keyword.tag-do', open: 'do', close: 'loop' },\r\n        { token: 'keyword.tag-for', open: 'for', close: 'next' }\r\n    ],\r\n    keywords: [\r\n        'AddHandler', 'AddressOf', 'Alias', 'And', 'AndAlso', 'As', 'Async', 'Boolean', 'ByRef', 'Byte', 'ByVal', 'Call',\r\n        'Case', 'Catch', 'CBool', 'CByte', 'CChar', 'CDate', 'CDbl', 'CDec', 'Char', 'CInt', 'Class', 'CLng',\r\n        'CObj', 'Const', 'Continue', 'CSByte', 'CShort', 'CSng', 'CStr', 'CType', 'CUInt', 'CULng', 'CUShort',\r\n        'Date', 'Decimal', 'Declare', 'Default', 'Delegate', 'Dim', 'DirectCast', 'Do', 'Double', 'Each', 'Else',\r\n        'ElseIf', 'End', 'EndIf', 'Enum', 'Erase', 'Error', 'Event', 'Exit', 'False', 'Finally', 'For', 'Friend',\r\n        'Function', 'Get', 'GetType', 'GetXMLNamespace', 'Global', 'GoSub', 'GoTo', 'Handles', 'If', 'Implements',\r\n        'Imports', 'In', 'Inherits', 'Integer', 'Interface', 'Is', 'IsNot', 'Let', 'Lib', 'Like', 'Long', 'Loop',\r\n        'Me', 'Mod', 'Module', 'MustInherit', 'MustOverride', 'MyBase', 'MyClass', 'NameOf', 'Namespace', 'Narrowing', 'New',\r\n        'Next', 'Not', 'Nothing', 'NotInheritable', 'NotOverridable', 'Object', 'Of', 'On', 'Operator', 'Option',\r\n        'Optional', 'Or', 'OrElse', 'Out', 'Overloads', 'Overridable', 'Overrides', 'ParamArray', 'Partial',\r\n        'Private', 'Property', 'Protected', 'Public', 'RaiseEvent', 'ReadOnly', 'ReDim', 'RemoveHandler', 'Resume',\r\n        'Return', 'SByte', 'Select', 'Set', 'Shadows', 'Shared', 'Short', 'Single', 'Static', 'Step', 'Stop',\r\n        'String', 'Structure', 'Sub', 'SyncLock', 'Then', 'Throw', 'To', 'True', 'Try', 'TryCast', 'TypeOf',\r\n        'UInteger', 'ULong', 'UShort', 'Using', 'Variant', 'Wend', 'When', 'While', 'Widening', 'With', 'WithEvents',\r\n        'WriteOnly', 'Xor'\r\n    ],\r\n    tagwords: [\r\n        'If', 'Sub', 'Select', 'Try', 'Class', 'Enum',\r\n        'Function', 'Get', 'Interface', 'Module', 'Namespace', 'Operator', 'Set', 'Structure', 'Using', 'While', 'With',\r\n        'Do', 'Loop', 'For', 'Next', 'Property', 'Continue', 'AddHandler', 'RemoveHandler', 'Event', 'RaiseEvent', 'SyncLock'\r\n    ],\r\n    // we include these common regular expressions\r\n    symbols: /[=><!~?;\\.,:&|+\\-*\\/\\^%]+/,\r\n    escapes: /\\\\(?:[abfnrtv\\\\\"']|x[0-9A-Fa-f]{1,4}|u[0-9A-Fa-f]{4}|U[0-9A-Fa-f]{8})/,\r\n    integersuffix: /U?[DI%L&S@]?/,\r\n    floatsuffix: /[R#F!]?/,\r\n    // The main tokenizer for our languages\r\n    tokenizer: {\r\n        root: [\r\n            // whitespace\r\n            { include: '@whitespace' },\r\n            // special ending tag-words\r\n            [/next(?!\\w)/, { token: 'keyword.tag-for' }],\r\n            [/loop(?!\\w)/, { token: 'keyword.tag-do' }],\r\n            // usual ending tags\r\n            [/end\\s+(?!for|do)([a-zA-Z_]\\w*)/, { token: 'keyword.tag-$1' }],\r\n            // identifiers, tagwords, and keywords\r\n            [/[a-zA-Z_]\\w*/, {\r\n                    cases: {\r\n                        '@tagwords': { token: 'keyword.tag-$0' },\r\n                        '@keywords': { token: 'keyword.$0' },\r\n                        '@default': 'identifier'\r\n                    }\r\n                }],\r\n            // Preprocessor directive\r\n            [/^\\s*#\\w+/, 'keyword'],\r\n            // numbers\r\n            [/\\d*\\d+e([\\-+]?\\d+)?(@floatsuffix)/, 'number.float'],\r\n            [/\\d*\\.\\d+(e[\\-+]?\\d+)?(@floatsuffix)/, 'number.float'],\r\n            [/&H[0-9a-f]+(@integersuffix)/, 'number.hex'],\r\n            [/&0[0-7]+(@integersuffix)/, 'number.octal'],\r\n            [/\\d+(@integersuffix)/, 'number'],\r\n            // date literal\r\n            [/#.*#/, 'number'],\r\n            // delimiters and operators\r\n            [/[{}()\\[\\]]/, '@brackets'],\r\n            [/@symbols/, 'delimiter'],\r\n            // strings\r\n            [/\"([^\"\\\\]|\\\\.)*$/, 'string.invalid'],\r\n            [/\"/, 'string', '@string'],\r\n        ],\r\n        whitespace: [\r\n            [/[ \\t\\r\\n]+/, ''],\r\n            [/(\\'|REM(?!\\w)).*$/, 'comment'],\r\n        ],\r\n        string: [\r\n            [/[^\\\\\"]+/, 'string'],\r\n            [/@escapes/, 'string.escape'],\r\n            [/\\\\./, 'string.escape.invalid'],\r\n            [/\"C?/, 'string', '@pop']\r\n        ],\r\n    },\r\n};\r\n\n\n//# sourceURL=webpack:///./node_modules/monaco-editor/esm/vs/basic-languages/vb/vb.js?");

/***/ })

}]);