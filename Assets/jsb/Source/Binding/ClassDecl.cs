﻿using System;
using QuickJS.Native;

namespace QuickJS.Binding
{
    public struct ClassDecl
    {
        public TypeRegister _register;
        private ScriptContext _ctx;
        public JSValue _ctor;
        public JSValue _proto;

        public ClassDecl(TypeRegister register, JSValue ctor, JSValue proto)
        {
            _register = register;
            _ctx = _register.GetContext();
            _ctor = ctor;
            _proto = proto;
        }

        public void Close()
        {
            JSApi.JS_FreeValue(_ctx, _ctor);
            JSApi.JS_FreeValue(_ctx, _proto);
            _ctor = JSApi.JS_UNDEFINED;
            _proto = JSApi.JS_UNDEFINED;
            _ctx = null;
        }

        public void AddMethod(bool bStatic, string name, JSCFunctionMagic func, int length, int magic)
        {
            var nameAtom = _register.GetAtom(name);
            var funcVal = JSApi.JSB_NewCFunctionMagic(_ctx, func, nameAtom, length, JSCFunctionEnum.JS_CFUNC_generic_magic,
                magic);
            JSApi.JS_DefinePropertyValue(_ctx, bStatic ? _ctor : _proto, nameAtom, funcVal, JSPropFlags.JS_PROP_C_W_E);
        }

        public void AddMethod(bool bStatic, string name, JSCFunction func, int length)
        {
            var nameAtom = _register.GetAtom(name);
            var funcVal = JSApi.JSB_NewCFunction(_ctx, func, nameAtom, length, JSCFunctionEnum.JS_CFUNC_generic, 0);
            JSApi.JS_DefinePropertyValue(_ctx, bStatic ? _ctor : _proto, nameAtom, funcVal, JSPropFlags.JS_PROP_C_W_E);
        }

        public void AddProperty(bool bStatic, string name, JSCFunction getter, JSCFunction setter)
        {
            var nameAtom = _register.GetAtom(name);
            var getterVal = JSApi.JS_UNDEFINED;
            var setterVal = JSApi.JS_UNDEFINED;
            var flags = JSPropFlags.JS_PROP_HAS_CONFIGURABLE | JSPropFlags.JS_PROP_HAS_ENUMERABLE;
            if (getter != null)
            {
                flags |= JSPropFlags.JS_PROP_HAS_GET;
                getterVal = JSApi.JSB_NewCFunction(_ctx, getter, nameAtom, 0, JSCFunctionEnum.JS_CFUNC_getter, 0);
            }

            if (setter != null)
            {
                flags |= JSPropFlags.JS_PROP_HAS_SET;
                setterVal = JSApi.JSB_NewCFunction(_ctx, setter, nameAtom, 0, JSCFunctionEnum.JS_CFUNC_setter, 0);
            }

            JSApi.JS_DefineProperty(_ctx, bStatic ? _ctor : _proto, nameAtom, JSApi.JS_UNDEFINED, getterVal, setterVal,
                flags);
        }

        #region 常量 (静态)
        public void AddConstValue(string name, bool v)
        {
            var val = JSApi.JS_NewBool(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, char v)
        {
            var val = JSApi.JS_NewInt32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, byte v)
        {
            var val = JSApi.JS_NewInt32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, sbyte v)
        {
            var val = JSApi.JS_NewInt32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, short v)
        {
            var val = JSApi.JS_NewInt32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, ushort v)
        {
            var val = JSApi.JS_NewInt32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, int v)
        {
            var val = JSApi.JS_NewInt32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, uint v)
        {
            var val = JSApi.JS_NewUint32(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, double v)
        {
            var val = JSApi.JS_NewFloat64(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, float v)
        {
            var val = JSApi.JS_NewFloat64(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }

        public void AddConstValue(string name, string v)
        {
            var val = JSApi.JS_NewString(_ctx, v);
            var nameAtom = _register.GetAtom(name);
            JSApi.JS_DefinePropertyValue(_ctx, _ctor, nameAtom, val, JSPropFlags.CONST_VALUE);
        }
        #endregion
    }
}