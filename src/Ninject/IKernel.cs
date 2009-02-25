﻿#region License
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
#region Using Directives
using System;
using System.Collections.Generic;
using Ninject.Activation;
using Ninject.Activation.Blocks;
using Ninject.Components;
using Ninject.Events;
using Ninject.Infrastructure.Disposal;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
#endregion

namespace Ninject
{
	/// <summary>
	/// A super-factory that can create objects of all kinds, following hints provided by <see cref="IBinding"/>s.
	/// </summary>
	public interface IKernel : IBindingRoot, IResolutionRoot, INotifyWhenDisposed
	{
		/// <summary>
		/// Gets the kernel settings.
		/// </summary>
		INinjectSettings Settings { get; }

		/// <summary>
		/// Gets the component container, which holds components that contribute to Ninject.
		/// </summary>
		IComponentContainer Components { get; }

		/// <summary>
		/// Occurs when a module is loaded.
		/// </summary>
		event EventHandler<ModuleEventArgs> ModuleLoaded;

		/// <summary>
		/// Occurs when a module is unloaded.
		/// </summary>
		event EventHandler<ModuleEventArgs> ModuleUnloaded;

		/// <summary>
		/// Determines whether a module of the specified type has been loaded in the kernel.
		/// </summary>
		/// <param name="moduleType">The type of the module.</param>
		/// <returns><c>True</c> if the specified module has been loaded; otherwise, <c>false</c>.</returns>
		bool HasModule(Type moduleType);

		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		/// <param name="module">The module to load.</param>
		void LoadModule(IModule module);

		/// <summary>
		/// Unloads the module with the specified type.
		/// </summary>
		/// <param name="moduleType">The type of the module.</param>
		void UnloadModule(Type moduleType);

		/// <summary>
		/// Injects the specified existing instance, without managing its lifecycle.
		/// </summary>
		/// <param name="instance">The instance to inject.</param>
		/// <param name="parameters">The parameters to pass to the request.</param>
		void Inject(object instance, params IParameter[] parameters);

		/// <summary>
		/// Gets the bindings that match the request.
		/// </summary>
		/// <param name="request">The request to match.</param>
		/// <returns>A series of bindings that match the request.</returns>
		IEnumerable<IBinding> GetBindings(IRequest request);

		/// <summary>
		/// Begins a new activation block, which can be used to deterministically dispose resolved instances.
		/// </summary>
		/// <returns>The new activation block.</returns>
		IActivationBlock BeginBlock();
	}
}
