import {InferActionType} from "../index";
import {SubjectInfoType, SubjectType} from "../../model/Subject/Subject";

export const initialState = {
  subjects: [] as SubjectType[],
  subjectsInfos: [] as SubjectInfoType[],
  error: '',
  loadingInitial: false,
  loading: false
};

export const subjectReducer = (state = initialState, action: SubjectActionType) => {
  switch (action.type) {
    case "SET_SUBJECTS":
      return {...state, subjects: action.payload};
    case "SET_SUBJECTS_INFOS":
      return {...state, subjectsInfos: action.payload};
    case 'SET_SUBJECT_LOADING':
      return {...state, loading: action.payload};
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
  setLoadingInitial: (payload: boolean) => ({
    type: 'SET_SUBJECT_LOADING_INITIAL',
    payload
  } as const)
};
