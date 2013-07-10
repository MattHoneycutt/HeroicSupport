﻿using System;

namespace HeroicSupport.Core.Utility
{
	public static class TypeExtensions
	{
		public static bool CanBeCastTo<TType>(this Type t)
		{
			return typeof(TType).IsAssignableFrom(t);
		}
	}
}