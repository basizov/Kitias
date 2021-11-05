import React, {useState} from 'react';
import {
  CSSObject,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItem,
  styled, Theme
} from "@mui/material";
import {ChevronLeft, Menu} from "@mui/icons-material";

const openMixin = (theme: Theme): CSSObject => ({
  width: 300,
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen
  }),
  overflowX: 'hidden'
});

const closeMixin = (theme: Theme): CSSObject => ({
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen
  }),
  overflowX: 'hidden',
  width: `calc(${theme.spacing(5)} + 1px)`,
  [theme.breakpoints.up('sm')]: {
    width: `calc(${theme.spacing(7)} + 1px)`
  }
});

const DrawerHeader = styled('div')(({theme}) => ({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  ...theme.mixins.toolbar
}));

const StyledLeftArrow = styled(DrawerHeader)({
  justifyContent: 'flex-end'
});

const StyledDrawer = styled(Drawer, {
  shouldForwardProp: (prop) => prop !== 'open'
})(({theme, open}) => ({
  width: 200,
  flexShrink: 0,
  whiteSpace: 'nowrap',
  boxSizing: 'border-box',
  ...(open && {
    ...openMixin(theme),
    '& .MuiDrawer-paper': openMixin(theme)
  }),
  ...(!open && {
    ...closeMixin(theme),
    '& .MuiDrawer-paper': closeMixin(theme)
  }),
}));

export const Sidebar: React.FC = () => {
  const [open, setOpen] = useState(false);

  return (
    <StyledDrawer variant="permanent" open={open}>
      {!open ?
        <DrawerHeader>
          <IconButton
            onClick={() => setOpen(true)}
          ><Menu/></IconButton>
        </DrawerHeader> :
        <StyledLeftArrow>
          <IconButton
            onClick={() => setOpen(false)}
          ><ChevronLeft/></IconButton>
        </StyledLeftArrow>}
      <Divider/>
      <List>
        <ListItem>1</ListItem>
        <ListItem>2</ListItem>
        <ListItem>3</ListItem>
      </List>
    </StyledDrawer>
  );
};
