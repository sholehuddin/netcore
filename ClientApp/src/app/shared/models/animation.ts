import {
  trigger,
  state,
  style,
  animate,
  transition
} from '@angular/animations'

import { PusintekUiModule } from '../../shared/pusintek-ui/pusintek-ui.module'

export const animationModule = [
  PusintekUiModule,
]

export const animation = [trigger('myState', [
      state(
        'inactive',
        style({
          transform: 'translate3d(0,0,0)',
          opacity: 0,
          display: 'none',
        })
      ),
      state(
        'active',
        style({
          transform: 'translate3d(10px,0,0)',
          opacity: 1,
          display: 'block',
        })
      ),
      transition('inactive => active', animate('300ms ease-in')),
      transition('active => inactive', animate('300ms ease-out'))
    ]),
    trigger('myRotation', [
      state(
        'inactive',
        style({
          // rotate left
          transform: 'rotate(0deg)'
        })
      ),
      state(
        'active',
        style({
          transform: 'rotate(360deg)'
        })
      ),
      transition('inactive => active', animate('600ms ease-in')),
      transition('active => inactive', animate('600ms ease-out'))
    ])]
