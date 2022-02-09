[![//_hyperscript](https://hyperscript.org/img/light_logo.png "the underscore is silent")](https://hyperscript.org)

*the underscore is silent*

## introduction

`_hyperscript` is a small, open scripting language inspired by [hypertalk](https://hypercard.org/HyperTalk%20Reference%202.4.pdf)

it is a companion project of <https://htmx.org>

## quickstart

```html

<script src="https://unpkg.com/hyperscript.org@0.9.4"></script>


<button _="on click toggle .clicked">
  Toggle the "clicked" class on me
</button>


<div hs="on mouseOver toggle mouse-over on #foo">
</div>

<div data-hs="on click call aJavascriptFunction() then
              wait 10s then
              call anotherJavascriptFunction()">
           Do some stuff
</div>
```

## website & docs

* <https://hyperscript.org>
* <https://hyperscript.org/docs>

## contributing

* please include test cases in [`/test`](https://github.com/bigskysoftware/_hyperscript/tree/dev/test) and docs in [`/www`](https://github.com/bigskysoftware/_hyperscript/tree/dev/www)
  * you can run the test suite by viewing `test/index.html` in a browser.
* development pull requests should be against the `dev` branch, docs fixes can be made directly against `master`
* you can build _hyperscript as shown: `npm run dist`. building is not necessary during development to run tests.
