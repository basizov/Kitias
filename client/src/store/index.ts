import {defaultReducer} from "./defaultStore";
import {applyMiddleware, combineReducers, createStore} from "redux";
import {composeWithDevTools} from "redux-devtools-extension";
import thunk from "redux-thunk";
import {attendanceReducer} from "./attendanceStore";
import {subjectReducer} from "./subjectStore";
import {groupReducer} from "./groupStore";

type PropertiesType<T> = T extends ({ [key: string]: infer U }) ? U : never;
export type InferActionType<T extends { [key: string]: (...args: any[]) => any }> = ReturnType<PropertiesType<T>>

const reducer = combineReducers({
  common: defaultReducer,
  attendance: attendanceReducer,
  subject: subjectReducer,
  group: groupReducer
});

export type RootState = ReturnType<typeof reducer>;
export const store = createStore(reducer, composeWithDevTools(applyMiddleware(thunk)));
