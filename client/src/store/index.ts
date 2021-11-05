import {defaultReducer} from "./defaultStore";
import {applyMiddleware, combineReducers, createStore} from "redux";
import {composeWithDevTools} from "redux-devtools-extension";
import thunk from "redux-thunk";

type PropertiesType<T> = T extends ({ [key: string]: infer U }) ? U : never;
export type InferActionType<T extends { [key: string]: (...args: any[]) => any }> = ReturnType<PropertiesType<T>>

const reducer = combineReducers({
  common: defaultReducer
});

export type RootState = ReturnType<typeof reducer>;
export const store = createStore(reducer, composeWithDevTools(applyMiddleware(thunk)));
