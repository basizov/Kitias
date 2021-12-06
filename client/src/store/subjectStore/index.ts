import {InferActionType} from "../index";
import {SubjectInfoType, SubjectType} from "../../model/Subject/Subject";
import {ShedulerType} from "../../model/Attendance/CreateShedulerModel";

export const initialState = {
  subjects: [] as SubjectType[],
  sheduler: null as ShedulerType | null,
  subjectsNames: [] as string[],
  subjectsInfos: [] as SubjectInfoType[],
  error: '',
  loadingInitial: false,
  loadingHelper: false,
  loading: false
};

export const subjectReducer = (state = initialState, action: SubjectActionType) => {
  switch (action.type) {
    case "SET_SUBJECTS":
      return {...state, subjects: action.payload};
    case "SET_SUBJECT_SHEDULER":
      return {...state, sheduler: action.payload};
    case "SET_SUBJECTS_NAMES":
      return {...state, subjectsNames: action.payload};
    case "SET_SUBJECTS_INFOS":
      return {...state, subjectsInfos: action.payload};
    case 'SET_SUBJECT_LOADING':
      return {...state, loading: action.payload};
    case 'SET_SUBJECT_LOADING_HELPER':
      return {...state, loadingHelper: action.payload};
    case 'SET_SUBJECT_LOADING_INITIAL':
      return {...state, loadingInitial: action.payload};
    case 'SET_SUBJECT_ERROR':
      return {...state, error: action.payload};
    default:
      return {...state};
  }
};

export type SubjectActionType = InferActionType<typeof subjectActions>;

export const subjectActions = {
  setSubjects: (payload: SubjectType[]) => ({
    type: 'SET_SUBJECTS',
    payload
  } as const),
  setSheduler: (payload: ShedulerType | null) => ({
    type: 'SET_SUBJECT_SHEDULER',
    payload
  } as const),
  setSubjectsNames: (payload: string[]) => ({
    type: 'SET_SUBJECTS_NAMES',
    payload
  } as const),
  setSubjectsInfos: (payload: SubjectInfoType[]) => ({
    type: 'SET_SUBJECTS_INFOS',
    payload
  } as const),
  setError: (payload: string) => ({
    type: 'SET_SUBJECT_ERROR',
    payload
  } as const),
  setLoading: (payload: boolean) => ({
    type: 'SET_SUBJECT_LOADING',
    payload
  } as const),
  setLoadingHelper: (payload: boolean) => ({
    type: 'SET_SUBJECT_LOADING_HELPER',
    payload
  } as const),
  setLoadingInitial: (payload: boolean) => ({
    type: 'SET_SUBJECT_LOADING_INITIAL',
    payload
  } as const)
};
