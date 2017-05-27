using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCE
{
    public class State
    {
        public static State True;
        public static State False;
        public static State Undefined;

        static State()
        {
            True = new State(true, false);
            False = new State(false, false);
            Undefined = new State(false, true);
        }

        private bool value;
        private bool undefined;

        private State(bool value, bool undefined)
        {
            this.value = value;
            this.undefined = undefined;
        }

        public static State boolToState(bool b)
        {
            return b ? State.True : State.False;
        }


        public static State Toggle(State operand)
        {
            if (operand == State.Undefined)
            {
                return State.True;
            }
            return boolToState(!operand.value);
        }

        public static State And(State operand1, State operand2)
        {
            if(operand1 == State.Undefined || operand2 == State.Undefined)
            {
                return State.Undefined;
            }
            return boolToState(operand1.value && operand2.value);
        }

    }
}
