﻿using System;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Core.Domain.UnitSystem;
using OSPSuite.Core.Extensions;

namespace OSPSuite.Core.Domain
{
   public static class WithDimensionExtensions
   {
      public static double ConvertToUnit(this IWithDimension withDimension, double value, Unit unit) 
      {
         return withDimension.Dimension?.BaseUnitValueToUnitValue(unit, value) ?? value;
      }

      public static double ConvertToUnit(this IWithDimension withDimension, double value, string unit)
      {
         if (withDimension.Dimension == null || !withDimension.Dimension.HasUnit(unit))
            return value;

         return ConvertToUnit(withDimension, value, withDimension.Dimension.Unit(unit));
      }

      public static bool IsConcentrationBased(this IWithDimension withDimension) => ReactionDimension(withDimension) == ReactionDimensionMode.ConcentrationBased;

      public static bool IsAmountBased(this IWithDimension withDimension) => ReactionDimension(withDimension) == ReactionDimensionMode.AmountBased;

      public static ReactionDimensionMode ReactionDimension(this IWithDimension withDimension)
      {
         return withDimension.DimensionIsOneOf(Constants.Dimension.MOLAR_CONCENTRATION, Constants.Dimension.MOLAR_CONCENTRATION_PER_TIME) ? ReactionDimensionMode.ConcentrationBased : ReactionDimensionMode.AmountBased;
      }

      public static bool IsAmount(this IWithDimension withDimension) => withDimension.DimensionIsOneOf(Constants.Dimension.AMOUNT, Constants.Dimension.MASS_AMOUNT);

      public static bool IsConcentration(this IWithDimension withDimension) => withDimension.DimensionIsOneOf(Constants.Dimension.MOLAR_CONCENTRATION, Constants.Dimension.MASS_CONCENTRATION);

      public static bool IsFraction(this IWithDimension withDimension) => withDimension.DimensionIsOneOf(Constants.Dimension.FRACTION);

      public static bool DimensionIsOneOf(this IWithDimension withDimension, params string[] dimensionNames) => withDimension.DimensionName().IsOneOf(dimensionNames);

      public static string DimensionName(this IWithDimension withDimension) => withDimension?.Dimension == null ? string.Empty : withDimension.Dimension.Name;

      public static string BaseUnitName(this IWithDimension withDimension) => withDimension?.Dimension?.BaseUnit.Name ?? string.Empty;
   }
}