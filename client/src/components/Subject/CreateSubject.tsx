import React, {useState} from 'react';
import {
  Button, ButtonGroup,
  Checkbox,
  FormControlLabel,
  Grid, List, ListItem, ListItemText,
  TextField
} from "@mui/material";
import {Form, Formik} from "formik";
import {CreateSubjectPairLecture} from "./CreateSubjectPairLecture";
import {CreateSubjectPairPractise} from "./CreateSubjectPairPractise";
import {CreateSubjectPairLaborotory} from "./CreateSubjectPairLaborotory";
import {CreateSubjectType} from "../../model/Subject/CreateSubjectModel";
import {createSubjects} from "../../store/subjectStore/asyncActions";

export const initialSubjectTypeState = {
  subjectName: '' as string,
  themes: true,
  newTheme: '' as string,
  themesList: [] as string[],
  lectureCount: 0 as number,
  lectureWeek: '' as string,
  lectureDay: '' as string,
  lectureTime: new Date(),
  lectureDates: [] as Date[],
  practiseCount: 0 as number,
  practiseWeek: '' as string,
  practiseDay: '' as string,
  practiseTime: new Date(),
  practiseDates: [] as Date[],
  laborotoryCount: 0 as number,
  laborotoryWeek: '' as string,
  laborotoryDay: '' as string,
  laborotoryTime: new Date(),
  laborotoryDates: [] as Date[]
} as const;

export const CreateSubject: React.FC = () => {
  const [newSubjects, setNewSubjects] = useState<CreateSubjectType[]>([]);

  return (
    <Formik
      initialValues={initialSubjectTypeState}
      onSubmit={(values) => {
        console.log(values);
      }}
    >
      {(props) => (
        <Form onSubmit={props.submitForm}>
          <Grid
            container
            spacing={1}
            direction='column'
            sx={{minWidth: '25rem'}}
          >
            <Grid container sx={{paddingTop: '0 !important'}}>
              <TextField
                id="subjectName"
                type="text"
                variant="outlined"
                fullWidth
                onBlur={props.handleBlur}
                value={props.values.subjectName}
                onChange={props.handleChange}
                onFocus={(e) => e.target.select()}
                error={!!props.errors.subjectName}
                label="Введите название предмета..."
              />
              <FormControlLabel
                sx={{marginLeft: 'auto'}}
                control={<Checkbox id='themes' defaultChecked/>}
                label="Использовать темы предметов"
                value={props.values.themes}
                onChange={props.handleChange}
              />
            </Grid>
            {props.values.themes && <Grid
                item
                sx={{padding: '0 !important', marginTop: '.3rem'}}
            >
                <TextField
                    id="newTheme"
                    type="text"
                    variant="outlined"
                    fullWidth
                    onBlur={props.handleBlur}
                    value={props.values.newTheme}
                    onChange={props.handleChange}
                    onFocus={(e) => e.target.select()}
                    error={!!props.errors.newTheme}
                    label="Введите новую тему..."
                />
            </Grid>}
            {props.values.themes && props.values.themesList.length > 0 &&
            <Grid
                item
                xs={12}
                sx={{
                  padding: '0 !important',
                  marginTop: '.5rem',
                  maxHeight: '5rem',
                  overflowY: 'auto'
                }}
            >
                <List disablePadding>
                  {props.values.themesList.map(theme => (
                    <ListItem disablePadding key={theme}>
                      <ListItemText primary={theme}/>
                    </ListItem>
                  ))}
                </List>
            </Grid>}
            <Grid container spacing={2} sx={{marginTop: '.3rem'}}>
              <Grid item xs={4}>
                <CreateSubjectPairLecture {...props}/>
              </Grid>
              <Grid item xs={4}>
                <CreateSubjectPairPractise {...props}/>
              </Grid>
              <Grid item xs={4}>
                <CreateSubjectPairLaborotory {...props}/>
              </Grid>
              <ButtonGroup
                variant='outlined'
                sx={{marginLeft: 'auto', marginTop: '.3rem'}}
              >
                {props.values.themes && <Button
                    onClick={() => {
                      props.setValues({
                        ...initialSubjectTypeState,
                        themesList: [props.values.newTheme, ...props.values.themesList]
                      });
                    }}
                >Добавить тему</Button>}
                <Button
                  onClick={async () => {
                    await createSubjects(newSubjects);
                  }}
                >Добавить предмет</Button>
              </ButtonGroup>
            </Grid>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
